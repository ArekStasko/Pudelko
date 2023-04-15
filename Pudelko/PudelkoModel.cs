using System.Collections;
using System.Diagnostics;

namespace Pudelko;

public class PudelkoModel: IFormattable, IEquatable<PudelkoModel>, IEnumerable<double>
{
    private UnitOfMeasure? _unit = UnitOfMeasure.milimeter;
    private double a = 0.1;
    private double b = 0.1;
    private double c = 0.1;

    public double A
    {
        get => a;
        set
        {
            a = CheckCondition(value);
        }
    }

    public double B
    {
        get => b;
        set
        {
           b = CheckCondition(value);
        }
    }

    public double C
    {
        get => c;
        set
        {
            c = CheckCondition(value);
        }
    }


    public PudelkoModel()
    {

    }

    public PudelkoModel(double a, double b, double c)
    {
        A = a;
        B = b;
        C = c;
    }
    
    public PudelkoModel(double a, double b, double c, UnitOfMeasure unit)
    {
        _unit = unit;
        A = a;
        B = b;
        C = c;
    }

    public PudelkoModel(double a, double b)
    {
        A = a;
        B = b;
    }
    
    public PudelkoModel(double a, double b, UnitOfMeasure unit)
    {
        _unit = unit;
        A = a;
        B = b;
    }

    public PudelkoModel(double a)
    {
        A = a;
    }
    
    public PudelkoModel(double a, UnitOfMeasure unit)
    {
        _unit = unit;
        A = a;
    }

    private double CheckCondition(double num)
    {
        if (num < 0.1) throw new ArgumentOutOfRangeException();
        
        double unit = _unit switch
        {
            UnitOfMeasure.milimeter => (num / 1000),
            UnitOfMeasure.centimeter => (num / 100),
            UnitOfMeasure.meter => num,
            _ => num
        };
        
        if (unit > 10) throw new ArgumentOutOfRangeException();
        
        return unit;
    }
    
    public double this[int i]
    {
        get
        {
            switch (i)
            {
                case 0:
                    return A;
                case 1:
                    return B;
                case 2:
                    return C;
                default:
                    throw new IndexOutOfRangeException();
            }
        }
    }
    
        private double ConvertToMeters(double number, UnitOfMeasure unit)
        {
            if (unit == UnitOfMeasure.milimeter)
            {
                return number / 1000;
            }
            else if (unit == UnitOfMeasure.centimeter)
            {
                return number / 100;
            }
            else
            {
                return number;
            }
        }
        public double Objetosc { get => Math.Round((A) * (B) * (C), 9); }
        public double Pole { get => Math.Round(2 * ((A) * (B) + (A) * (C) + (B) * (C)), 6); }

        
        public PudelkoModel(double? _a = null, double? _b = null, double? _c = null, UnitOfMeasure? unit = null)
        {
            if (unit == null) 
            {
                unit = UnitOfMeasure.meter;
            }
            if (_a == null)
            {
                C = 0.1;
                unit = UnitOfMeasure.meter;
            }
            else if (_b == null)
            {
                B = 0.1;
                unit = UnitOfMeasure.meter;
            }
            else if (_c == null)
            {
                C = 0.1;
                unit = UnitOfMeasure.meter;
            }

            A = ConvertToMeters((double)_a, (UnitOfMeasure)unit);
            B = ConvertToMeters((double)_b, (UnitOfMeasure)unit);
            C = ConvertToMeters((double)_c, (UnitOfMeasure)unit);

            if (A <= 0 || A > 10 || B <= 0 || B > 10 || C <= 0 | C > 10)
            {
                throw new ArgumentOutOfRangeException();
            }

            _unit = unit;

        }
        public override string ToString()
        {
            return ToString("m");
        }

        public string ToString(string? format)
        {
            return ToString(format, null);
        }

        public string ToString(string? format, IFormatProvider formatProvider)
        {
            var formatType = format ?? "m";
            switch (formatType)
            {
                case "cm": 
                    return $"{String.Format("{0:0.0}", A * 100)} cm × {String.Format("{0:0.0}", B * 100)} cm × {String.Format("{0:0.0}", C * 100)} cm";
                case "mm":
                    return $"{A * 1000} mm × {B * 1000} mm × {C * 1000} mm";
                case "m": 
                    return $"{String.Format("{0:0.000}", A)} m × {String.Format("{0:0.000}", B)} m × {String.Format("{0:0.000}", C)} m";
                default:
                    throw new FormatException();
            }
        }

        public bool Equals(PudelkoModel? other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            if (obj is PudelkoModel)
            {
                return Equals(obj as PudelkoModel);
            }
            else
            {
                return Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            return A.GetHashCode() + B.GetHashCode() + C.GetHashCode();
        }

        public static bool operator ==(PudelkoModel p1, PudelkoModel p2) => p1.Equals(p2);
        public static bool operator !=(PudelkoModel p1, PudelkoModel p2) => p1.Equals(p2);

        public static implicit operator PudelkoModel(ValueTuple<int, int, int, UnitOfMeasure> p) => new PudelkoModel(p.Item1, p.Item2, p.Item3, UnitOfMeasure.milimeter); //niejawne (krotka)

        public static PudelkoModel operator +(PudelkoModel p1, PudelkoModel p2)
        {
            return new PudelkoModel(
              p1[0] + p2[0],
              p1[1] + p2[1],
              p1[2] + p2[2],
              UnitOfMeasure.meter
          );
        }


        List<double> odcinki = new List<double> { 5.0, 6.0, 7.0 }; 
        public IEnumerator<double> GetEnumerator()
        {
            foreach (var x in odcinki)
            {
                yield return x;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => odcinki.GetEnumerator();

        public static PudelkoModel Parsowanie(string parse)
        {
            UnitOfMeasure unit = UnitOfMeasure.meter;
            string[] parseSplit = parse.Split(' ');
            double a = double.Parse(parseSplit[0]);
            double b = double.Parse(parseSplit[8]);
            double c = double.Parse(parseSplit[16]);

            if (parse.Contains("mm"))
            {
                unit = UnitOfMeasure.milimeter;
            }
            else if (parse.Contains('m'))
            {
                unit = UnitOfMeasure.meter;
            }
            else if (parse.Contains("cm"))
            {
                unit = UnitOfMeasure.centimeter;
            }

            return new PudelkoModel(a, b, c, unit);
        }

        public static int Porownaj(PudelkoModel p1, PudelkoModel p2)
        {
            if (p1.Objetosc == p2.Objetosc || p1.Pole == p2.Pole || p1.A + p1.B + p1.C == p2.A + p2.B + p2.C)
            {
                return 0;
            }
            else if (p1.Objetosc < p2.Objetosc)
            {
                return 1;
            }
            else if (p1.Pole < p2.Pole)
            {
                return 1;
            }
            else if (p1.A + p1.B + p1.C < p2.A + p2.B + p2.C)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

    public static explicit operator double[](PudelkoModel b) => new []{b.A, b.B, b.C};
    public static implicit operator PudelkoModel(ValueTuple<int, int, int> tpl) => new (tpl.Item1, tpl.Item2, tpl.Item3);
}