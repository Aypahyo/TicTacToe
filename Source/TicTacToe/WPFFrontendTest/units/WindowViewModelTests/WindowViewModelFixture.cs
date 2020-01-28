using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TicTacToe.WPFFrontend;

namespace TicTacToe.WPFFrontendTest.units.WindowViewModelTests
{
    [TestFixture]
    public class WindowViewModelFixture
    {
        private WindowViewModel _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new WindowViewModel();
        }

        [TestCaseSource(typeof(WindowViewModelData), nameof(WindowViewModelData.ShouldRaiseWhenMapIsSetData))]
        public void ShouldRaiseWhenMapIsSet()
        {
            var args = new List<PropertyChangedEventArgs>();
            _uut.PropertyChanged += (s, e) => args.Add(e);

        }

    }
}
