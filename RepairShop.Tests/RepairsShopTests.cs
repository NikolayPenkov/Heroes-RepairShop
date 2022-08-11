using NUnit.Framework;
using System;

namespace RepairShop.Tests
{
    public class Tests
    {
        public class RepairsShopTests
        {
            [Test]
            public void GarageNameShouldThrowExceptionIsNullOrEmpty()
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    //Arrange & Act
                    var garage = new Garage(null, 1);
                },
                //Assert
                "Invalid garage name.");

                Assert.Throws<ArgumentNullException>(() =>
                {
                    //Arrange & Act
                    var garage = new Garage(null, 1);
                },
               //Assert
               "Invalid garage name.");
            }

            [Test]
            public void GarageNameShouldWorkCorrectly()
            {
                //Arrange && Act
                const string TestName = "Test";
                var garage = new Garage(TestName, 2);

                //Assert
                Assert.That(garage.Name, Is.EqualTo(TestName));
            }
            [Test]
            public void IfMechanicAvaliablesAreZeroShouldThrowArgumentException()
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    //Arrange && Act
                    const string TestName = "Test";
                    var garage = new Garage(TestName, 0);
                },
               //Assert
               "At least one mechanic must work in the garage.");

                Assert.Throws<ArgumentException>(() =>
                {
                    //Arrange && Act
                    const string TestName = "Test";
                    var garage = new Garage(TestName, -1);
                },
             //Assert
             "At least one mechanic must work in the garage.");
            }
            [Test]
            public void MechanicsAvaliableShouldWorkCorrect()
            {
                //Arrange && Act
                const string TestName = "Test";
                const int TestGarageMechanics = 5;
                var garage = new Garage(TestName, TestGarageMechanics);

                //Assert
                Assert.That(garage.MechanicsAvailable, Is.EqualTo(TestGarageMechanics));
            }
            [Test]
            public void IfMechanicsInMethodAddCarAreEqualToTheCarsThrowInvalidOperationException()
            {
                //Arrange
                const int TestGarageMechanics = 1;
                const string TestName = "Test";
                var garage = new Garage(TestName, TestGarageMechanics);
                var firstCar = new Car("First test car", 3);
                var secondCar = new Car("Second test car", 2);
                //Act
                garage.AddCar(firstCar);
                //Assert
                Assert.Throws<InvalidOperationException>(() =>
                {
                    garage.AddCar(secondCar);
                },
                "No mechanic available.");
            }
            [Test]
            public void MethodAddCarShouldWorkProperly()
            {
                //Arrange
                const int TestGarageMechanics = 2;
                const string TestName = "Test";
                var garage = new Garage(TestName, TestGarageMechanics);
                var firstCar = new Car("First test car", 3);
                var secondCar = new Car("Second test car", 2);

                //Assert
                Assert.That(garage.MechanicsAvailable, Is.EqualTo(2));
            }
            [Test]
            public void GarageFixCarShouldWorkCorectly()
            {
                //Arrange
                const string carModel = "Test Car";
                var garage = new Garage("Test", 5);
                var carOne = new Car("Car1", 5);
                var cartwo = new Car("Car2", 3);
                var carThree = new Car(carModel, 2);
                //Act
                garage.AddCar(carThree);
                var fixedCar = garage.FixCar(carModel);

                //Assert
                Assert.That(fixedCar.IsFixed, Is.True);
                Assert.That(fixedCar.CarModel, Is.EqualTo(carModel));
                Assert.That(fixedCar.NumberOfIssues, Is.EqualTo(0));
            }
            public void GarageFixCarShouldThrowExceptionIfThereAreNoCarsToBeFixed()
            {
                //Arrange
                const string carModel = "Test Car";
                var garage = new Garage("Test", 5);
                var carOne = new Car("Car1", 5);
                var cartwo = new Car("Car2", 3);
                var carThree = new Car(carModel, 2);

                garage.AddCar(carThree);
                var fixedCar = garage.FixCar(carModel);

                //Assert
                Assert.Throws<InvalidOperationException>(() =>
                {
                    //Act
                    garage.RemoveFixedCar();
                },
                "No fixed cars available.");
            }
            [Test]
            public void GarageRemoveFixCarShouldWorkCorectly()
            {
                //Arrange
                const string carModel = "Test Car";

                var garage = new Garage("Test", 5);
                var carOne = new Car("Car1", 5);
                var carTwo = new Car("Car2", 3);
                var carThree = new Car(carModel, 2);

                //Act
                garage.AddCar(carOne);
                garage.AddCar(carTwo);
                garage.AddCar(carThree);
                garage.FixCar(carModel);
                var fixedCar = garage.RemoveFixedCar();
                //Assert
                Assert.That(fixedCar, Is.EqualTo(1));
                Assert.That(garage.CarsInGarage, Is.EqualTo(2));
            }
            [Test]
            public void GarageReportShouldWorkCorrectly()
            {
                //Arrange
                const string carModel = "Test Car";

                var garage = new Garage("Test", 5);
                var carOne = new Car("Car1", 5);
                var carTwo = new Car("Car2", 3);
                var carThree = new Car(carModel, 2);

                //Act
                garage.AddCar(carOne);
                garage.AddCar(carTwo);
                garage.AddCar(carThree);
                garage.FixCar(carModel);
                var report = garage.Report();

                //Assert
                Assert.That(report, Is.EqualTo($"There are 2 which are not fixed: Car1, Car2."));
            }
        }
    }
}