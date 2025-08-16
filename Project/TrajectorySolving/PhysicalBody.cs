namespace TrajectorySolving
{
    public class PhysicalBody
    {
        private readonly double _semiMajorAxis;
        private readonly double _massOfBody;
        private readonly double _massOfSecondBody;
        private readonly double _orbitalPeriod;
        private readonly double _eccentricity;
        private const double E0 = 0;
        private const double eps = 1e-7;
        public double SemiMajorAxis { get { return _semiMajorAxis; } }
        public double MassOfBody { get { return _massOfBody; } }
        public double MassOfSecondBody { get { return _massOfSecondBody; } }
        public double OrbitalPeriod { get { return _orbitalPeriod; } }
        public double Eccentricity { get { return _eccentricity; } }
        public PhysicalBody(double semiMajorAxis, double massOfBody, double massOfSecondBody, double orbitalPeriod, double eccentricity)
        {
            _semiMajorAxis = semiMajorAxis;
            _massOfBody = massOfBody;
            _massOfSecondBody = massOfSecondBody;
            _orbitalPeriod = orbitalPeriod;
            _eccentricity = eccentricity;
        }
        public (double X, double Y) GetCoordinates(double t)
        {
            double E = EccentricAnomaly(t);
            double X = _semiMajorAxis * (Math.Cos(E) - _eccentricity);
            double Y = _semiMajorAxis * Math.Sqrt(1 - _eccentricity * _eccentricity) * Math.Sin(E);

            return (X, Y);
        }
        private double MeanAnomaly(double t) => 2 * Math.PI * t / _orbitalPeriod;
        private double KeplerEquation(double E, double M) => E - (_eccentricity * Math.Sin(E) + M);
        private double KeplerEquationDerivative(double E) => 1 - _eccentricity * Math.Cos(E);
        private double EccentricAnomaly(double t)
        {
            double M = MeanAnomaly(t);
            double E1, E2 = E0;
            do
            {
                E1 = E2;
                E2 = E1 - KeplerEquation(E1, M) / KeplerEquationDerivative(E1);
            } while (Math.Abs(E1 - E2) > eps);

            return E2;
        }
    }
}
