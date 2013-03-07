using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using PdfCraft.Constants;
using PdfCraft.Contents;
using PdfCraft.Contents.Graphics;
using PdfCraft.Extensions;

namespace PdfCraft.API
{
    public class GraphicsCanvas : ContentsPart
    {
        private readonly Document _owner;
        private readonly List<GraphicsCommand> _graphicsCommands = new List<GraphicsCommand>();
        private readonly HashSet<string> _xObjectReferences = new HashSet<string>();


        public GraphicsCanvas(Document owner)
        {
            _owner = owner;
        }

        internal override bool IsText { get { return false; } }

        internal Size Size { get; set; }

        public void SetFillColor(Color color)
        {
            _graphicsCommands.Add(new GraphicsCommand(Command.SetFillColor, color));
        }

        public void SetStrokeColor(Color color)
        {
            _graphicsCommands.Add(new GraphicsCommand(Command.SetStrokeColor, color));
        }

        public void DrawLine(Point start, Point end)
        {
            var lineDefinition = new LineDefinition(start.GetPointInMillimeters(), end.GetPointInMillimeters());
            _graphicsCommands.Add(new GraphicsCommand(Command.DrawLine, lineDefinition));
        }

        public void DrawBox(Point topLeft, Size size)
        {
            var boxDefinition = new BoxDefinition(topLeft.GetPointInMillimeters(), size.GetSizeInMillimeters());
            _graphicsCommands.Add(new GraphicsCommand(Command.DrawBox, boxDefinition));
        }

        public void DrawCircle(Point center, int radius)
        {
            var circleDefinition = new CircleDefinition(center.GetPointInMillimeters(), radius.ToMillimeters());
            _graphicsCommands.Add(new GraphicsCommand(Command.DrawCircle, circleDefinition));
        }

        public void DrawImage(Point topLeft, Size size, ImageType imageType, string sourceFile)
        {
            var imageDefinition = new ImageDefinition(topLeft, size, imageType, sourceFile);
            _graphicsCommands.Add(new GraphicsCommand(Command.DrawImage, imageDefinition));
        }

        public void DrawImage(Point topLeft, Size size, ImageType imageType, Stream sourceStream)
        {
            var imageDefinition = new ImageDefinition(topLeft, size, imageType, sourceStream);
            _graphicsCommands.Add(new GraphicsCommand(Command.DrawImage, imageDefinition));
        }

        public IEnumerable<string> GetXObjectnames()
        {
            return _xObjectReferences;
        }

        internal override string Content
        {
            get
            {
                var sb = new StringBuilder("0.5 w" + StringConstants.NewLine);

                foreach (var command in _graphicsCommands)
                {
                    switch (command.Command)
                    {
                        case Command.SetFillColor:
                            var fillColor = (Color)command.Data;
                            sb.Append(fillColor.ToPdfColor() + " rg ");
                            break;
                        case Command.SetStrokeColor:
                            var strokeColor = (Color)command.Data;
                            sb.Append(strokeColor.ToPdfColor() + " RG ");
                            break;
                        case Command.DrawLine:
                            var lineDefinition = (LineDefinition)command.Data;
                            var startY = Size.Height - lineDefinition.Start.Y;
                            var endY = Size.Height - lineDefinition.End.Y;
                            sb.Append(string.Format("{0} {1} m {2} {3} l S ", lineDefinition.Start.X, startY, lineDefinition.End.X, endY));
                            break;
                        case Command.DrawBox:
                            var boxDefinition = (BoxDefinition)command.Data;
                            var topLeftY = Size.Height - boxDefinition.TopLeft.Y - boxDefinition.Size.Height;
                            sb.Append(string.Format("{0} {1} {2} {3} re S ", boxDefinition.TopLeft.X, topLeftY, boxDefinition.Size.Width, boxDefinition.Size.Height));
                            break;
                        case Command.DrawCircle:
                            const double kappa = 0.552284749830794;

                            var circle = (CircleDefinition)command.Data;
                            var Xc = circle.Center.X;
                            var Yc = Size.Height - circle.Center.Y;
                            var R = circle.Radius;
                            var l = (int)Math.Round(kappa * R, 3);

                            //Xc Yc+R m 
                            //Xc+l Yc+R Xc+R Yc+l Xc+R Yc c 
                            //Xc+R Yc-l Xc+l Yc-R Xc Yc-R c 
                            //Xc-l Yc-R Xc-R Yc-l Xc-R Yc c 
                            //Xc-R Yc+l Xc-l Yc+R Xc Yc+R c B
                            const string circleFormat = "{0} {1} m {2} {1} {3} {4} {3} {5} c {3} {6} {2} {7} {0} {7} c {8} {7} {9} {6} {9} {5} c {9} {4} {8} {1} {0} {1} c B ";
                            sb.Append(string.Format(circleFormat, Xc, Yc + R, Xc + l, Xc + R, Yc + l, Yc, Yc - l, Yc - R, Xc - l, Xc - R));

                            break;
                        case Command.DrawImage:
                            var imageDefinition = (ImageDefinition)command.Data;

                            XObject xObject;
                            if (imageDefinition.SourceFile != null)
                                xObject = _owner.AddXObject(imageDefinition.ImageType, imageDefinition.SourceFile);
                            else
                                xObject = _owner.AddXObject(imageDefinition.ImageType, imageDefinition.SourceStream);

                            var imageReference = string.Format("{0} {1} 0 R ", xObject.XObjectname, xObject.Number);
                            _xObjectReferences.Add(imageReference);

                            const string imageFormat = "q {0} 0 0 {1} {2} {3} cm {4} Do Q ";
                            sb.Append(string.Format(imageFormat,
                                imageDefinition.Size.Width,
                                imageDefinition.Size.Height,
                                imageDefinition.TopLeft.X,
                                imageDefinition.TopLeft.Y,
                                xObject.XObjectname));

                            break;
                    }
                }

                return sb.ToString();
            }
        }
    }

    public class ImageDefinition
    {
        private readonly Point _topLeft;
        private readonly Size _size;
        private readonly ImageType _imageType;
        private readonly Stream _sourceStream;
        private readonly string _sourceFile;

        public ImageDefinition(Point topLeft, Size size, ImageType imageType, string sourceFile)
            : this(topLeft, size, imageType)
        {
            _sourceFile = sourceFile;
        }

        public ImageDefinition(Point topLeft, Size size, ImageType imageType, Stream sourceStream)
            : this(topLeft, size, imageType)
        {
            _sourceStream = sourceStream;
        }

        public ImageDefinition(Point topLeft, Size size, ImageType imageType)
        {
            _topLeft = topLeft;
            _size = size;
            _imageType = imageType;
        }

        public Point TopLeft
        {
            get { return _topLeft; }
        }

        public Size Size
        {
            get { return _size; }
        }

        public ImageType ImageType
        {
            get { return _imageType; }
        }

        public Stream SourceStream
        {
            get { return _sourceStream; }
        }

        public string SourceFile
        {
            get { return _sourceFile; }
        }
    }
}