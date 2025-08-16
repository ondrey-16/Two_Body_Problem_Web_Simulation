using TrajectorySolving;

namespace TrajectorySolvingTest
{
    public class PhysicalBodyTest
    {
        [Fact]
        public void Test_Earth_Perihelium()
        {
            PhysicalBody earth = new PhysicalBody(semiMajorAxis: PhysicalConstants.Au, massOfBody: PhysicalConstants.Me, 
                massOfSecondBody: PhysicalConstants.Ms, orbitalPeriod: PhysicalConstants.T, eccentricity: PhysicalConstants.Ee);
            double r = 1.47098e11;
            var result = earth.GetCoordinates(t: 0);
            double rResult = Math.Sqrt(result.X * result.X + result.Y * result.Y);

            Assert.Equal(r, rResult, 1e7);
            Assert.Equal(0, result.Y, 1e7);
        }
        [Fact]
        public void Test_Earth_Aphelium()
        {
            PhysicalBody earth = new PhysicalBody(semiMajorAxis: PhysicalConstants.Au, massOfBody: PhysicalConstants.Me,
                massOfSecondBody: PhysicalConstants.Ms, orbitalPeriod: PhysicalConstants.T, eccentricity: PhysicalConstants.Ee);
            double r = 1.52098e11;
            var result = earth.GetCoordinates(t: 182.62818);
            double rResult = Math.Sqrt(result.X * result.X + result.Y * result.Y);

            Assert.Equal(r, rResult, 1e7);
            Assert.Equal(0, result.Y, 1e7);
        }
        [Fact]
        public void Test_Earth_ExactAU()
        {
            PhysicalBody earth = new PhysicalBody(semiMajorAxis: PhysicalConstants.Au, massOfBody: PhysicalConstants.Me,
                massOfSecondBody: PhysicalConstants.Ms, orbitalPeriod: PhysicalConstants.T, eccentricity: PhysicalConstants.Ee);
            
            double E1 = Math.PI / 2, E2 = 3 * Math.PI / 2;
            double M = 2 * Math.PI / PhysicalConstants.T;
            double t1 = (E1 - PhysicalConstants.Ee * Math.Sin(E1)) / M;
            double t2 = (E2 - PhysicalConstants.Ee * Math.Sin(E2)) / M;

            var result1 = earth.GetCoordinates(t: t1);
            double rResult1 = Math.Sqrt(result1.X * result1.X + result1.Y * result1.Y);
            var result2 = earth.GetCoordinates(t: t2);
            double rResult2 = Math.Sqrt(result2.X * result2.X + result2.Y * result2.Y);

            Assert.Equal(PhysicalConstants.Au, rResult1, 1e7);
            Assert.Equal(PhysicalConstants.Au, rResult2, 1e7);
        }
    }
}
