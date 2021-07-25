using Logger;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestFixture]
    public class LoggerClassTest
    {
        [Test]
        [TestCase("Writter", "Salji podatke direktno...")]
        [TestCase("Reader", "Citaj iz baze...")]
        [TestCase("DumpingBuffer", "DumpingBuffer prima podatke od Writtera.")]
        [TestCase("Historical", "Primi podatke od writera.")]
        [TestCase("Historical", "Upisano u bazu...")]


        public void LoggerSlucajevi_Dobar(string from, string message)
        {
            LoggerClass log = new LoggerClass();
            log.LogEvent(from, message);
        }

        [Test]
        [TestCase("Writter", null)]
        [TestCase("Historical", null)]
        [TestCase(null, "Send some data to Historical.")]
        [TestCase(null, "Read data from Database.")]
        [TestCase(null, null)]

        public void LoggerSlucajevi_Los(string from, string message)
        {
            LoggerClass log = new LoggerClass();
            Assert.Throws<ArgumentNullException>(() =>
            {
                log.LogEvent(from, message);
            });
        }

        [Test]
        [TestCase("Writter", "")]
        [TestCase("Historical", "")]
        [TestCase("", "Send some data to Historical.")]
        [TestCase("", "radi")]
        [TestCase("", "")]

        public void LoggerSlucajevi_Los2(string from, string message)
        {
            LoggerClass log = new LoggerClass();
            Assert.Throws<ArgumentException>(() =>
            {
                log.LogEvent(from, message);
            });
        }

        [Test]
        [TestCase("w", "Send some data to Dumping Buffer.")]
        [TestCase("h", "Receive some data to Database.")]
        [TestCase("db", "Send some data to Historical.")]
        [TestCase("r", "Read data from Database.")]
        [TestCase("d", "Write some data.")]

        public void LoggerSlucajevi_Los3(string from, string message)
        {
            LoggerClass log = new LoggerClass();
            Assert.Throws<ArgumentException>(() =>
            {
                log.LogEvent(from, message);
            });
        }
    }
}
