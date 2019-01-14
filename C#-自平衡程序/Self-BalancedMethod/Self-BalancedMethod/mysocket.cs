using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using System.Text.RegularExpressions;//正则表达式
using System.Runtime.InteropServices;//Marshal内存分配

using System.Runtime.Serialization;//序列化
using System.Runtime.Serialization.Formatters.Binary;//序列化
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace Self_BalancedMethod {
    class mysocket {
        TcpClient tcp = null;
        NetworkStream workstream = null;

        // static StateObject state = new StateObject();
        class StateObject
        {
            public TcpClient client = null;
            public int totalBytesRead = 0;
            public const int BufferSize = 1600;
            public string readType = null;
            public byte[] buffer = new byte[BufferSize];
            public StringBuilder messageBuffer = new StringBuilder();

        }

        public void SendData(byte[] data, int len)
        {
            try
            {
                if (workstream != null)
                {
                    workstream.Write(data, 0, len);
                    workstream.Flush();
                    System.Threading.Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        delegate void SetTextCallback(string text);
        delegate void SetControl();
        delegate void GetData(byte[] data);

        public void OnGetData(byte[] data)
        {

        }

        public ManualResetEvent connectDone = new ManualResetEvent(false);


        public void ConnectCallback(IAsyncResult ar)
        {
            connectDone.Set();
            TcpClient t = (TcpClient)ar.AsyncState;
            try
            {
                if (t.Connected)
                {
                    t.EndConnect(ar);
                    //MainFrame.mfs.SetConnect_text(UsrLibs.usrlib.ReadXML_ID("SetupTSI.xml", "language", "已连接",MainFrame.mfs. language)[0]);
                    Main.mfs.SetConnect_state(1);
                    // Console.WriteLine("已连接");
                }
                else
                {
                    t.EndConnect(ar);
                    //MainFrame.mfs.SetConnect_text(UsrLibs.usrlib.ReadXML_ID("SetupTSI.xml", "language", "已断开",MainFrame.mfs. language)[0]);
                    Main.mfs.SetConnect_state(0);
                    //Console.WriteLine("已断开");
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine("ConnectCallback excp=" + t.Connected.ToString());//se.ToString()
                MessageBox.Show(ex.Message);
            }
        }

        public void Connect()
        {
            if ((tcp == null) || (!tcp.Connected)) //网络没有连接的情况下，下面尝试连接主控板ARM
            {
                try
                {
                    tcp = new TcpClient();
                    //tcp.ReceiveTimeout = 10;

                    connectDone.Reset();
                    string ip_str;
                    ip_str = "192.168.0.2";//默认是这个IP地址，固定了，用户可以根据需要修改，比如主界面的编辑框里的字符串付给ip_str
                    tcp.BeginConnect(ip_str, 1001,new AsyncCallback(ConnectCallback), tcp);//端口号1001 固定。

                    connectDone.WaitOne(1000);//1s超时，设置连接
                    if(tcp.Connected == true) {
                         MessageBox.Show("连接成功！");
                    } else {
                        MessageBox.Show("连接失败！");
                    }
                   
                    if ((tcp != null) && (tcp.Connected))
                    {
                        workstream = tcp.GetStream();//标准的TCP连接操作
                        asyncread(tcp);//标准的tCP连接操作函数
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connect() excp");//se.ToString() 如果异常，会打印信息
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void DisConnect()
        {
            if ((tcp != null) && (tcp.Connected))//
            {
                workstream.Close();//标准的TCP断开操作
                tcp.Close();
                Main.mfs.SetConnect_state(0);
            }
        }

        public void asyncread(TcpClient sock)//标准的异步连接TCP函数，不能修改
        {
            StateObject state = new StateObject();
            state.client = sock;
            NetworkStream stream = sock.GetStream();
            if (stream.CanRead)
            {
                try
                {
                    IAsyncResult ar = stream.BeginRead(state.buffer, 0,
                    StateObject.BufferSize, new AsyncCallback(TCPReadCallBack), state);//设置回调函数为：TCPReadCallBack，会执行这个函数
                }
                catch (Exception ex)
                {
                    MessageBox.Show("asyncread excp");
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void ReadData()//读网口操作
        {
            if ((tcp != null) && (tcp.Connected))
                asyncread(tcp);
        }
        public void TCPReadCallBack(IAsyncResult ar)//回调函数，TCP接收导数据会进入到这里。
        {
            StateObject state = null;
            try
            {
                state = (StateObject)ar.AsyncState;
            }
            catch
            {
                return;
            }
            if ((state.client == null) || (!state.client.Connected))
                return;
            int numberofBytesRead;
            try
            {
                NetworkStream mas = state.client.GetStream();
                // string type = null;
                numberofBytesRead = mas.EndRead(ar); // 获得网口有的数据量
                state.totalBytesRead += numberofBytesRead;
                if (numberofBytesRead > 0)
                {
                    byte[] dd = new byte[numberofBytesRead];
                    Array.Copy(state.buffer, 0, dd, 0, numberofBytesRead);//把从网口读到的数据复制给dd数组
                    mas.BeginRead(state.buffer, 0, StateObject.BufferSize, new AsyncCallback(TCPReadCallBack), false);//标准调用
                    Main.mfs.process_receive(dd, numberofBytesRead);//处理dd数组中接收到的数据
                    // MessageBox.Show("Get len = " + numberofBytesRead.ToString());
                    // 内存释放 这个不管用
                    //dd = null;
                    // GC.Collect(); //强制对所有代进行垃圾回收
                }
                else
                {
                    mas.Close();
                    state.client.Close();
                    mas = null;
                    state = null;
                    // MainFrame.mfs.SetConnect_text(UsrLibs.usrlib.ReadXML_ID("SetupTSI.xml", "language", "已断开", MainFrame.mfs.language)[0]);
                    Main.mfs.SetConnect_state(0);
                }
                mas.Close();
                state.client.Close();
                mas = null;
                state = null;
            }
            catch
            {
                return;
            }
            // 内存释放 这个也不管用
            // state = null;
            // GC.Collect(); //强制对所有代进行垃圾回收
        }
    }
}
