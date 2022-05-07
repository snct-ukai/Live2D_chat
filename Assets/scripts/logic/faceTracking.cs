using UnityEngine;
using OpenCvSharp;
using DlibDotNet;
using System.Runtime.InteropServices;

public class faceTracking
{
    private faceTracking() { }
    private static faceTracking _instance = new faceTracking();
    public static faceTracking getInstance() => _instance;

    public int cameraID = 0;

    public void tracking()
    {
        //Mat�I�u�W�F�N�g��web�J�����摜��ǂݍ���
        Mat mat = new Mat();
        VideoCapture capture = new VideoCapture();
        capture.Open(cameraID);
        capture.Read(mat);

        //�摜�̃��T�C�Y
        Mat output = new Mat();
        Cv2.Resize(mat, output, new Size(320, 180));
        mat.Dispose();

        //DlibDotNet�ɉ摜��ǂނ���
        byte[] array = new byte[output.Width * output.Height * output.ElemSize()];
        Marshal.Copy(output.Data, array, 0, array.Length);

        Array2D<RgbPixel> image = Dlib.LoadImageData<RgbPixel>(array, (uint)output.Height, (uint)output.Width, (uint)(output.Width * output.ElemSize()));
        output.Dispose();

        //�猟�o
        FrontalFaceDetector detector = Dlib.GetFrontalFaceDetector();
        Rectangle[] rectangles = detector.Operator(image);


    }
    
}
