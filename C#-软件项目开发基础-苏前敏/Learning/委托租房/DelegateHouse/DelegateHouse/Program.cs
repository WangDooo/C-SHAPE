using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateHouse {
    // 枚举HouseTpye
    public enum HouseType {
        OneBedroom,
        TwoBedroom,
        ThreeBedroom,
    };
    
    class Program {
        // 定义租房委托
        public delegate void LetAgencyToFind(HouseType wantType, double rent);
        // 定义房子类
        public class House {
            private HouseType _roomingTpye;
            public HouseType RoomingType {
                get{return _roomingTpye;}
                set{_roomingTpye = value;}
            }
            private double _rent;
            public double Rent {
                get{return _rent;}
                set{_rent = value;}
            }
            public House(HouseType roomingType, double rent) {
                this._roomingTpye = roomingType;
                this._rent = rent;
            }
        }
        // 定义客户并调用委托
        public class Customer {
            private string _name;
            public LetAgencyToFind agencyDelegate;
            public HouseType RoomingType{get;set;}
            public double Rent{get;set;}
            public Customer(string name, HouseType roomingType, double rent) {
                this._name = name;
                this.RoomingType = roomingType;
                this.Rent = rent;
            }
            public void ToRentHouse() {
                Console.WriteLine("\n{0}说，要找{1}的房子，租金不超过{2}元", _name, RoomingType, Rent);
                agencyDelegate(RoomingType, Rent);
            }
        }
        // 定义中介类，实现委托的实际方法找房
        class Agency {
            private string _name;
            public string Name {
                get{ return _name;}
                set{ _name = value;}
            }
            private List<House> houses = new List<House>();
            public Agency(string name) {
                this._name = name;
                House h1 = new House(HouseType.OneBedroom, 1500);
                House h2 = new House(HouseType.TwoBedroom, 1800);
                House h3 = new House(HouseType.TwoBedroom, 2200);
                houses.Add(h1);
                houses.Add(h2);
                houses.Add(h3);
            }
            public void FindHouse(HouseType roomingType, double rent) {
                Console.WriteLine("\n中介《{0}》开始找房...", this.Name);
                List<string> houseString = new List<string>();//创建了一个空列表
                List<double> houseDouble = new List<double>();
                foreach (House h in houses) {
                    if(h.RoomingType == roomingType && h.Rent <= rent) {
                        houseString.Add(h.RoomingType.ToString());
                        houseDouble.Add(h.Rent);
                    }
                }
                if( houseString.Count > 0) { 
                    Console.WriteLine("一共找到{0}套房",houseString.Count );
                    for(int i = 0; i<houseString.Count; i++) {
                        Console.WriteLine("{0}.类型：{1}，租金：{2}",i+1, houseString[i], houseDouble[i]);
                    }
                } else {
                    Console.WriteLine("没有房源");
                }
            }
        }

        static void Main(string[] args) {
            Customer cust = new Customer("WangDoo", HouseType.TwoBedroom, 2200);
            Agency anAgency = new Agency("找房子网");
            cust.agencyDelegate = new LetAgencyToFind(anAgency.FindHouse);
            cust.ToRentHouse();
            Console.ReadLine();
        }
    }
}
