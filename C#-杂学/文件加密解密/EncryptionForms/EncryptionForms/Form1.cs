using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Globalization;

namespace EncryptionForms {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            // 利用SaveFileDialog，让用户指定文件的路径名
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "文本文件|*.txt";
            if (saveDlg.ShowDialog() == DialogResult.OK)
            { 

                FileEncryptDecrypt.Encrypt(saveDlg.FileName,textBox1.Text);

                // 提示用户：文件保存的位置和文件名
                MessageBox.Show("文件已成功保存到" + saveDlg.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            string filepath = @"C:\DDD\GitHub\C-SHAPE\C#-杂学\文件加密解密\test2.txt";
            MessageBox.Show(FileEncryptDecrypt.Decrypt(filepath));
        }

        private void button3_Click(object sender, EventArgs e) {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "文本文件|*.txt";
            if (saveDlg.ShowDialog() == DialogResult.OK)
            { 
                FileStream fs = new FileStream(saveDlg.FileName, FileMode.Append, FileAccess.Write);
                // string encrypt = Encrypt(textBox2.Text);
                string encrypt = EncryptString(textBox2.Text);
                byte[] data = System.Text.Encoding.Default.GetBytes(encrypt);
                //开始写入
                fs.Write(data, 0, data.Length);
                //清空缓冲区、关闭流
                fs.Flush();
                fs.Close();
                // 提示用户：文件保存的位置和文件名
                MessageBox.Show("文件已成功保存到" + saveDlg.FileName);
            }

        }

        private void button4_Click(object sender, EventArgs e) {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false; //该值确定是否可以选择多个文件
            dialog.Title = "请选择文件夹";
            dialog.Filter = "文本文件|*.txt";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string file = dialog.FileName;
                FileStream fs = new FileStream(file, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string line = sr.ReadToEnd();
                // string decrypt = Decrypt(line);
                string decrypt = DecryptString(line);
                textBox3.Text = decrypt;
            }
        }

        private void button5_Click(object sender, EventArgs e) {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "文本文件|*.txt";
            if (saveDlg.ShowDialog() == DialogResult.OK)
            { 
                FileStream fs = new FileStream(saveDlg.FileName, FileMode.Append, FileAccess.Write);
                byte[] result = Encoding.UTF8.GetBytes(textBox2.Text);
                string encrypt = BitConverter.ToString(result);
                byte[] data = System.Text.Encoding.Default.GetBytes(encrypt);
                //开始写入
                fs.Write(data, 0, data.Length);
                //清空缓冲区、关闭流
                fs.Flush();
                fs.Close();
                // 提示用户：文件保存的位置和文件名
                MessageBox.Show("文件已成功保存到" + saveDlg.FileName);
            }
        }

        private void button6_Click(object sender, EventArgs e) {

        }

        public static string Key = "WangDooo";

        public static string EncryptString(string sInputString)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(sInputString);
 
                DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
 
                DES.Key = ASCIIEncoding.ASCII.GetBytes(Key);
 
                DES.IV = ASCIIEncoding.ASCII.GetBytes(Key);
 
                ICryptoTransform desencrypt = DES.CreateEncryptor();
 
                byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
 
                return BitConverter.ToString(result);
            }
            catch { }
 
            return "转换出错！";
        }

        public static string DecryptString(string sInputString)
        {
            try
            {
                string[] sInput = sInputString.Split("-".ToCharArray());
 
                byte[] data = new byte[sInput.Length];
 
                for (int i = 0; i < sInput.Length; i++)
                {
                    data[i] = byte.Parse(sInput[i], NumberStyles.HexNumber);
                }
 
                DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
 
                DES.Key = ASCIIEncoding.ASCII.GetBytes(Key);
 
                DES.IV = ASCIIEncoding.ASCII.GetBytes(Key);
 
                ICryptoTransform desencrypt = DES.CreateDecryptor();
 
                byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
 
                return Encoding.UTF8.GetString(result);
            }
            catch { }
 
            return "解密出错！";
        }

        public static string Encrypt(string sourceString)
        {
            try
            {
                byte[] btKey = Encoding.UTF8.GetBytes(Key);
 
                byte[] btIV = Encoding.UTF8.GetBytes(Key);
 
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
 
                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] inData = Encoding.UTF8.GetBytes(sourceString);
                    try
                    {
                        using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(btKey, btIV), CryptoStreamMode.Write))
                        {
                            cs.Write(inData, 0, inData.Length);
 
                            cs.FlushFinalBlock();
                        }
 
                        return Convert.ToBase64String(ms.ToArray());
                    }
                    catch
                    {
                        return sourceString;
                    }
                }
            }
            catch { }
 
            return "DES加密出错";
        }

        public static string Decrypt(string encryptedString)
        {
            byte[] btKey = Encoding.UTF8.GetBytes(Key);
 
            byte[] btIV = Encoding.UTF8.GetBytes(Key);
 
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
 
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Convert.FromBase64String(encryptedString);
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
 
                        cs.FlushFinalBlock();
                    }
 
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
                catch
                {
                    return encryptedString;
                }
            }
        }

        
    }



    /// <summary>
    /// 文件加密
    /// </summary>
    public class FileEncryptDecrypt
    {

        /// <summary>
        /// 加密字符
        /// </summary>
         private static string key = "KANGJINW";

        /// <summary>
        /// 将密钥加密写入到文件
        /// </summary>
        /// <param name="sInputFilename">密密钥</param>
        /// <param name="sOutputFilename">密钥文件路径</param>
        /// <param name="sKey"></param>
        private  static void EncryptFile(string sInputFilename,string sOutputFilename,string sKey)
        {
            FileStream fsEncrypted = new FileStream(sOutputFilename, FileMode.Append, FileAccess.Write);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Mode = CipherMode.ECB; //这里指定加密模式为ECB
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            ICryptoTransform desencrypt = DES.CreateEncryptor();
            CryptoStream cryptostream = new CryptoStream(fsEncrypted,  desencrypt, CryptoStreamMode.Write);
            byte[] fsInput = System.Text.Encoding.Default.GetBytes(sInputFilename);
            cryptostream.Write(fsInput, 0, fsInput.Length);
            cryptostream.Close();
            fsEncrypted.Close();
        }
        /// <summary>
        /// 打开密钥文件
        /// </summary>
        /// <param name="sInputFilename">密钥路径</param>
        /// <param name="sKey"></param>
        static string DecryptFile(string sInputFilename, string sKey)
        {
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Mode = CipherMode.ECB; //这里指定加密模式为ECB
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);

            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.Padding = System.Security.Cryptography.PaddingMode.Zeros; // None

            FileStream fsread = new FileStream(sInputFilename,  FileMode.Open,  FileAccess.Read);

            ICryptoTransform desdecrypt = DES.CreateDecryptor();

            CryptoStream cryptostreamDecr = new CryptoStream(fsread,  desdecrypt,  CryptoStreamMode.Read);
            StreamReader read = new StreamReader(cryptostreamDecr, System.Text.Encoding.Default);
            string reft = read.ReadToEnd();
            fsread.Flush();
            fsread.Close();
            return reft;
        }
        /// <summary>
        /// 加密文件
        /// </summary>
        /// <param name="filename">文件存放路径</param>
        /// <param name="soce">加密内容</param>
        public static void Encrypt(string filename, string soce)
        {
            EncryptFile(soce, filename, key);
            //try
            //{
               
            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}

        }
        /// <summary>
        /// 解密文件
        /// </summary>
        /// <param name="filename">打开文件路径</param>
        /// <returns>返回加密文件的内容</returns>
        public static string Decrypt(string filename)
        {
            return DecryptFile(filename, key);
            //try
            //{

            //    return DecryptFile(filename, key);

            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}
        }
    }
}
