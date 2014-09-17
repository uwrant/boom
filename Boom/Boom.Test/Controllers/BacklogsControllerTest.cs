using Boom.Domain;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using Xunit;

namespace Boom.Controllers.Test
{
    // run "k test" in command line in Boom.Test folder to execute the tests
    public class BacklogsControllerTest
    {
        [Fact]
        public void Get_SingleBacklog_BacklogReturned()
        {
            // Arrange
            var controller = new BacklogsController();

            // Act
            var backlogActionResult = controller.Get() as JsonResult;

            // Assert
            Assert.NotNull(backlogActionResult);
            var backlogs = (Backlog[])backlogActionResult.Data;
            Assert.Equal(1, backlogs.Length);
            var backlog = backlogs[0];
            Assert.Equal("TestBacklog", backlog.Name);
        }
    }
}
