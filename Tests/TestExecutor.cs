using System;

namespace Tests
{
    public class TestExecutor
    {
        private readonly BaseTest _test;

        public TestExecutor(BaseTest test)
        {
            _test = test;
        }

        public void Assert(Action assertion)
        {
            _test.Act();

            assertion();
        }
    }
}