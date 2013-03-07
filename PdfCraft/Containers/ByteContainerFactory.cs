namespace PdfCraft.Containers
{
    public static class ByteContainerFactory
    {
         public static IByteContainer CreateByteContainer()
         {
             return new ByteArrayByteContainer();
         }

         public static IByteContainer CreateByteContainer(string text)
         {
             return new ByteArrayByteContainer(text);
         }

        public static IByteContainer CreateByteContainer(byte[] bytes)
        {
            return new ByteArrayByteContainer(bytes);
        }
    }
}