using SavaDev.MigrationsMaker;
using System.ComponentModel.DataAnnotations;
using System;

namespace SavaDev.DevTools.Tests.MigrationsMaker
{
    public class MigrationsProfileTests : IDisposable
    {
        private IEnumerable<ServiceInfo> _solutionServices;

        public MigrationsProfileTests() 
        {
            _solutionServices = new ServiceInfo[]
            {
                new ServiceInfo("Blogs", ""),
                new ServiceInfo("Files", ""),
                new ServiceInfo("Media", "")
            };
        }

        public void Dispose()
        {
            _solutionServices = null;
        }

        [Fact]
        public void Cmd_Service()
        {
            // Arrange
            var command = "Blogs";

            // Act
            var profile = new MigrationsProfile(command.Split(" "), _solutionServices);

            // Assert
            Assert.False(profile.Reset);
            Assert.True(profile.MigrationsName?.StartsWith("Auto"));
            Assert.NotNull(profile.Services);
            Assert.Equal(1, profile.Services?.Count());
        }

        [Fact]
        public void Cmd_Service_Lower()
        {
            // Arrange
            var command = "blogs";

            // Act
            var profile = new MigrationsProfile(command.Split(" "), _solutionServices);

            // Assert
            Assert.False(profile.Reset);
            Assert.True(profile.MigrationsName?.StartsWith("Auto"));
            Assert.NotNull(profile.Services);
            Assert.Equal(1, profile.Services?.Count());
        }

        [Theory]
        [InlineData("Blogs MyMigration")]
        [InlineData("MyMigration Blogs")]
        public void Cmd_Service_Name(string command)
        {
            // Arrange

            // Act
            var profile = new MigrationsProfile(command.Split(" "), _solutionServices);

            // Assert
            Assert.False(profile.Reset);
            Assert.Equal("MyMigration", profile.MigrationsName);
            Assert.NotNull(profile.Services);
            Assert.Equal(1, profile.Services?.Count());
        }

        [Fact]
        public void Cmd_Name()
        {
            // Arrange
            var command = "MyMigration";

            // Act
            var action = () => new MigrationsProfile(command.Split(" "), _solutionServices);

            // Assert
            Assert.Throws<IncorrectCommandException>(action);
        }

        [Theory]
        [InlineData("Blogs MyMigration Any")]
        [InlineData("Any Blogs MyMigration")]
        [InlineData("Any MyMigration Blogs")]
        public void Cmd_Service_Name_Any(string command)
        {
            // Arrange

            // Act
            var action = () => new MigrationsProfile(command.Split(" "), _solutionServices);

            // Assert
            Assert.Throws<IncorrectCommandException>(action);
        }

        [Fact]
        public void Cmd_All()
        {
            // Arrange
            var command = "All";

            // Act
            var profile = new MigrationsProfile(command.Split(" "), _solutionServices);

            // Assert
            Assert.False(profile.Reset);
            Assert.True(profile.MigrationsName?.StartsWith("Auto"));
            Assert.NotNull(profile.Services);
            Assert.Equal(_solutionServices.Count(), profile.Services?.Count());
        }

        [Fact]
        public void Cmd_All_Lower()
        {
            // Arrange
            var command = "all";

            // Act
            var profile = new MigrationsProfile(command.Split(" "), _solutionServices);

            // Assert
            Assert.False(profile.Reset);
            Assert.True(profile.MigrationsName?.StartsWith("Auto"));
            Assert.NotNull(profile.Services);
            Assert.Equal(_solutionServices.Count(), profile.Services?.Count());
        }

        [Fact]
        public void Cmd_All_Name()
        {
            // Arrange
            var command = "All MyMigration";

            // Act
            var profile = new MigrationsProfile(command.Split(" "), _solutionServices);

            // Assert
            Assert.False(profile.Reset);
            Assert.Equal("MyMigration", profile.MigrationsName);
            Assert.NotNull(profile.Services);
            Assert.Equal(_solutionServices.Count(), profile.Services?.Count());
        }

        [Theory]
        [InlineData("All MyMigration Any")]
        [InlineData("Any All MyMigration")]
        [InlineData("Any MyMigration All")]
        public void Cmd_All_Name_Any(string command)
        {
            // Arrange

            // Act
            var action = () => new MigrationsProfile(command.Split(" "), _solutionServices);

            // Assert
            Assert.Throws<IncorrectCommandException>(action);
        }

        [Theory]
        [InlineData("All Blogs")]
        [InlineData("Blogs All")]
        [InlineData("All Blogs MyMigration")]
        public void Cmd_All_Service(string command)
        {
            // Arrange

            // Act
            var action = () => new MigrationsProfile(command.Split(" "), _solutionServices);

            // Assert
            Assert.Throws<IncorrectCommandException>(action);
        }

        [Fact]
        public void Cmd_Reset()
        {
            // Arrange
            var command = "Reset";

            // Act
            var profile = new MigrationsProfile(command.Split(" "), _solutionServices);

            // Assert
            Assert.True(profile.Reset);
            Assert.Equal("Initial", profile.MigrationsName);
            Assert.NotNull(profile.Services);
            Assert.Equal(_solutionServices.Count(), profile.Services?.Count());
        }

        [Fact]
        public void Cmd_Reset_Lower()
        {
            // Arrange
            var command = "reset";

            // Act
            var profile = new MigrationsProfile(command.Split(" "), _solutionServices);

            // Assert
            Assert.True(profile.Reset);
            Assert.Equal("Initial", profile.MigrationsName);
            Assert.NotNull(profile.Services);
            Assert.Equal(_solutionServices.Count(), profile.Services?.Count());
        }

        [Theory]
        [InlineData("Reset Blogs")]
        [InlineData("Blogs Reset")]
        public void Cmd_Reset_Service(string command)
        {
            // Arrange

            // Act
            var profile = new MigrationsProfile(command.Split(" "), _solutionServices);

            // Assert
            Assert.True(profile.Reset);
            Assert.Equal("Initial", profile.MigrationsName);
            Assert.NotNull(profile.Services);
            Assert.Equal(1, profile.Services?.Count());
        }

        [Theory]
        [InlineData("Reset MyMigration")]
        [InlineData("MyMigration Reset")]
        public void Cmd_Reset_Name(string command)
        {
            // Arrange

            // Act
            var profile = new MigrationsProfile(command.Split(" "), _solutionServices);

            // Assert
            Assert.True(profile.Reset);
            Assert.Equal("MyMigration", profile.MigrationsName);
            Assert.NotNull(profile.Services);
            Assert.Equal(_solutionServices.Count(), profile.Services?.Count());
        }

        [Theory]
        [InlineData("Reset Blogs MyMigration")]
        [InlineData("Blogs MyMigration Reset")]
        [InlineData("Reset MyMigration Blogs")]
        public void Cmd_Reset_Service_Name(string command)
        {
            // Arrange

            // Act
            var profile = new MigrationsProfile(command.Split(" "), _solutionServices);

            // Assert
            Assert.True(profile.Reset);
            Assert.Equal("MyMigration", profile.MigrationsName);
            Assert.NotNull(profile.Services);
            Assert.Equal(1, profile.Services?.Count());
        }

        [Theory]
        [InlineData("Reset All")]
        [InlineData("All Reset")]
        public void Cmd_Reset_All(string command)
        {
            // Arrange

            // Act
            var profile = new MigrationsProfile(command.Split(" "), _solutionServices);

            // Assert
            Assert.True(profile.Reset);
            Assert.Equal("Initial", profile.MigrationsName);
            Assert.NotNull(profile.Services);
            Assert.Equal(_solutionServices.Count(), profile.Services?.Count());
        }

        [Theory]
        [InlineData("Reset All MyMigration")]
        [InlineData("All MyMigration Reset")]
        public void Cmd_Reset_All_Name(string command)
        {
            // Arrange

            // Act
            var profile = new MigrationsProfile(command.Split(" "), _solutionServices);

            // Assert
            Assert.True(profile.Reset);
            Assert.Equal("MyMigration", profile.MigrationsName);
            Assert.NotNull(profile.Services);
            Assert.Equal(_solutionServices.Count(), profile.Services?.Count());
        }

        [Theory]
        [InlineData("Reset All Blogs")]
        public void Cmd_Reset_All_Service(string command)
        {
            // Arrange

            // Act
            var action = () => new MigrationsProfile(command.Split(" "), _solutionServices);

            // Assert
            Assert.Throws<IncorrectCommandException>(action);
        }

        [Theory]
        [InlineData("")]
        [InlineData("A B C D")]
        public void Cmd_ArgsCount(string command)
        {
            // Arrange

            // Act
            var action = () => new MigrationsProfile(command.Split(" "), _solutionServices);

            // Assert
            Assert.Throws<IncorrectCommandException>(action);
        }

        [Fact]
        public void Constructor_ArgsNull()
        {
            // Arrange

            // Act
            var action = () => new MigrationsProfile(null, _solutionServices);

            // Assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void Constructor_ServicesNull()
        {
            // Arrange
            var command = "reset";

            // Act
            var action = () => new MigrationsProfile(command.Split(" "), null);

            // Assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Theory]
        [InlineData("MyMigration")]
        [InlineData("MyMigration Any")]
        [InlineData("MyMigration Some Any")]
        public void Cmd_Any(string command)
        {
            // Arrange

            // Act
            var action = () => new MigrationsProfile(command.Split(" "), _solutionServices);

            // Assert
            Assert.Throws<IncorrectCommandException>(action);
        }
    }
}