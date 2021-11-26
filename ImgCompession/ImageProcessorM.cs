using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing;
using ImageProcessor;
using ImageProcessor.Processors;
using ImageProcessor.Imaging;
using ImageProcessor.Imaging.Formats;

namespace ImgCompession
{
    class ImageProcessorM
    {
        BitmapImage input_image;
        BitmapImage output_image;
        int columns_size_px=10;
        public ImageProcessorM()
        {

        }
        public BitmapImage ToImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }
        public byte[] getJPGFromImageControl(BitmapImage imageC)
        {
            MemoryStream memStream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(imageC));
            encoder.Save(memStream);
            return memStream.ToArray();
        }
        public static BitmapSource ToBitmapSource( byte[] bytes)
        {

            using (var stream = new MemoryStream(bytes))
            {
                var decoder = new JpegBitmapDecoder(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                return decoder.Frames[0];
            }
        }
        public BitmapSource process(BitmapImage input_image, int columns_size_px, string algorithm)
        {
            List<Point> total_res= new List<Point>();
            Point[] points = this.img_to_points(input_image);
            //var all=new[]
            for (int col=0; col< input_image.PixelWidth; col++)
            {
                Point[] temp = new Point[input_image.PixelHeight];
                var index = col * input_image.PixelHeight;
                Array.Copy(points, index, temp, 0, input_image.PixelHeight);
                var res = this.aproximate_col(temp); //todo add this to global array
                total_res.AddRange(res);
            }
            var out_pixels = new List<byte>();
            for (int i=0; i<total_res.Count; i++)
            {
                out_pixels.AddRange(this.point_to_pixel(total_res[i]));
            }
            //BitmapImage out_img = this.ToImage(out_pixels.ToArray());
            //return out_img;
            var x= getJPGFromImageControl(input_image);
            var y= ToBitmapSource(x);
            return y;
          
            // return input_image;
        }
        
        public Point[] aproximate_col(Point[] column)
        {
            var res = SimplifyUtility.SimplifyArray(column);
            return column;
        }
        public BitmapImage getImage()
        {
            return this.output_image;
        }
        public Point pixel_to_point(int index, byte[] pixel)
        {
            double color = (pixel[0] + pixel[1] + pixel[2]) / 3;
            return new Point(index, color);
        }
        public byte[] point_to_pixel(Point point)
        {
            byte[] pixel = new byte[4];
            pixel[0] = pixel[1] = pixel[2] = (byte)point.Y;
            pixel[3] = 255;
            return pixel;

        }
       
        public Point[] img_to_points(BitmapImage img)
        {
            int stride = img.PixelWidth * 4;
            int size = img.PixelHeight * stride;
            byte[] pixels = new byte[size];
            Point[] points = new Point[img.PixelWidth* img.PixelHeight];
            img.CopyPixels(pixels, stride, 0);
            for (int x=0; x< img.PixelWidth; x++)
            {
                for (int y = 0; y < img.PixelHeight; y++)
                {
                    int index = y * stride + 4 * x;
                    byte[] temp = new byte[4];
                    Array.Copy(pixels, index, temp, 0, 4);
                    Point p = this.pixel_to_point(y, temp);
                    points[x * img.PixelHeight + y] = p;
                }
            }
            return points;

        }
        public void f()
        {
            byte[] photoBytes = File.ReadAllBytes(file);
            // Format is automatically detected though can be changed.
            ISupportedImageFormat format = new JpegFormat { Quality = 70 };
            Size size = new Size(150, 0)
using (MemoryStream inStream = new MemoryStream(photoBytes))
            {
                using (MemoryStream outStream = new MemoryStream())
                {
                    // Initialize the ImageFactory using the overload to preserve EXIF metadata.
                    using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                    {
                        // Load, resize, set the format and quality and save an image.
                        imageFactory.Load(inStream)
                                    .Resize(size)
                                    .Format(format)
                                    .Save(outStream);
                    }
                    // Do something with the stream.
                }
            }
        }

        
    }
}



