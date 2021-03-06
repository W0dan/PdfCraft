﻿using NUnit.Framework;
using PdfCraft.Containers;

namespace Tests.Concerning_the_ByteArrayByteContainer.Given_a_non_empty_ByteArrayByteContainer
{
    public abstract class Non_empty_ByteArrayByteContainer : BaseTest
    {
        protected ByteArrayByteContainer Sut;
        protected string OriginalContent;

        [TestFixtureSetUp]
        public override void Setup()
        {
            base.Setup();
        }

        public override void Arrange()
        {
            OriginalContent = "this is non empty";
            Sut = new ByteArrayByteContainer(OriginalContent);
        }
    }
}