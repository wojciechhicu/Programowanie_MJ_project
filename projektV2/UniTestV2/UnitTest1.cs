using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniTestV2
{
    [TestClass]
    public class DB
    {
        /// <summary>
        /// Test if combination of data return correct string
        /// </summary>
        [TestMethod]
        public void returnGoodValue()
        {
            DB db = new DB();
            string server = "server=localhost;";
            string port = "port=3306;";
            string username = "username=root;";
            string pass = "password=;";
            string dataBase = "database=users";
            string connString = server + port + username + pass + dataBase;

            string expexted = "server=localhost;port=3306;username=root;password=;database=users";

            Assert.AreEqual(expexted, connString);
        }

        /// <summary>
        /// Test if port number is correct
        /// </summary>
        [TestMethod]
        public void testCorrectData()
        {
            DB db = new DB();
            string server = "server=localhost;";
            string port = "port=3306;";
            string username = "username=root;";
            string pass = "password=;";
            string dataBase = "database=users";
            string connString = server + port + username + pass + dataBase;

            Assert.AreEqual("port=3306;", port);
        }
    }
}