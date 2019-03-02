using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_BalancedMethod {
    [Serializable]
    public class DataClass {
        private double _number;
        private double _time;
        private double _Q;
        private double _s;
        private double _Qu;
        private double _Qd;
        private double _sd;
        public DataClass(double Number,double Time, double Q, double s, double Qu, double Qd, double sd) {
            this._number = Number;
            this._time = Time;
            this._Q = Q;
            this._s = s;
            this._Qu = Qu;
            this._Qd = Qd;
            this._sd = sd;
        }
        public double Number {
            get{ return _number; }
        }
        public double Time {
            get{ return _time; }
        }
        public double Q {
            get{ return _Q; }
        }
        public double s {
            get{ return _s; }
        }
        public double Qu {
            get{ return _Qu; }
        }
        public double Qd {
            get{ return _Qd; }
        }
        public double sd {
            get{ return _sd; }
        }

    }
}
