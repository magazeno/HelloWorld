﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using HalconDotNet;

namespace HalconCamera
{
    /// <summary>
    /// HALCON接口名称
    /// </summary>
    public enum HInterfaceName
    {
        /*1394IIDC, 1394IIDC-2,*/ /*ABS, ADLINK, Andor, BitFlow, Crevis, DahengCAM,*/
        DirectFile, DirectShow, File, GenICamTL, GigEVision, /*Ginga++,*/ GingaDG,
        INSPECTA, INSPECTA5, LinX, LPS36, LuCam, MatrixVisionAcquire, MILLite,
        MultiCam, Opteon, p3i2, PcEyeCL, PixeLINK, pylon, SaperaLT, Sentech, /*SICK-3DCamera,*/
        SiliconSoftware, /*SonyXCI-2,*/ TWAIN, uEye, VRmUsbCam
    }

    public enum FieldMode { @default, first, second, next, interlaced, progressive }

    public enum ColorSpaceMode { @default, gray, raw, rgb, yuv }

    public enum ExternalTriggerMode { @default, @true, @false }

    public enum CameraTypeMode { @default, ntsc, pal, auto }



    /// <summary>
    /// 接口信息
    /// </summary>
    public class HInterfaceInfo
    {
        public HInterfaceInfo()
        {

        }

        public HInterfaceInfo(HInterfaceName interfaceName)
        {
            InterfaceName = interfaceName;
            HTuple name = Enum.GetName(typeof(HInterfaceName), interfaceName);

            HTuple information;
            HOperatorSet.InfoFramegrabber(name, "info_boards", out information, out InfoBoards);

            if (InfoBoards.Length > 0)
            {
                HOperatorSet.InfoFramegrabber(name, "horizontal_resolution", out information, out HorizontalResolution);
                HOperatorSet.InfoFramegrabber(name, "vertical_resolution", out information, out VerticalResolution);
                HOperatorSet.InfoFramegrabber(name, "image_width", out information, out ImageWidth);
                HOperatorSet.InfoFramegrabber(name, "image_height", out information, out ImageHeight);
                HOperatorSet.InfoFramegrabber(name, "start_row", out information, out StartRow);
                HOperatorSet.InfoFramegrabber(name, "start_column", out information, out StartColumn);
                HOperatorSet.InfoFramegrabber(name, "field", out information, out Field);
                HOperatorSet.InfoFramegrabber(name, "bits_per_channel", out information, out BitsPerChannel);
                HOperatorSet.InfoFramegrabber(name, "color_space", out information, out ColorSpace);
                HOperatorSet.InfoFramegrabber(name, "generic", out information, out Generic);
                HOperatorSet.InfoFramegrabber(name, "external_trigger", out information, out ExternalTrigger);
                HOperatorSet.InfoFramegrabber(name, "camera_type", out information, out CameraType);
                HOperatorSet.InfoFramegrabber(name, "device", out information, out Device);
                HOperatorSet.InfoFramegrabber(name, "port", out information, out Port);
                HOperatorSet.InfoFramegrabber(name, "line_in", out information, out LineIn);

                HOperatorSet.InfoFramegrabber(name, "parameters", out information, out Parameters);
                HOperatorSet.InfoFramegrabber(name, "parameters_readonly", out information, out ParametersReadonly);
                HOperatorSet.InfoFramegrabber(name, "parameters_writeonly", out information, out ParametersWriteonly);

                HOperatorSet.InfoFramegrabber(name, "defaults", out information, out Defaults);
                HOperatorSet.InfoFramegrabber(name, "general", out information, out General);
                HOperatorSet.InfoFramegrabber(name, "revision", out information, out Revision);
            }
        }

        public HInterfaceName InterfaceName;         //HALCON图像获取接口

        public HTuple HorizontalResolution; //水平分辨率
        public HTuple VerticalResolution;   //垂直分辨率
        public HTuple ImageWidth;           //图像宽度
        public HTuple ImageHeight;          //图像高度
        public HTuple StartRow;             //开始行
        public HTuple StartColumn;          //开始列
        public HTuple Field;                //半图像或全图像 "first", "second", "next", "interlaced", "progressive", "default" 
        public HTuple BitsPerChannel;       //每个像素的传输位和图像通道的数量
        public HTuple ColorSpace;           //颜色空间 "gray", "raw", "rgb", "yuv", "default" 
        public HTuple Generic;              //通用参数
        public HTuple ExternalTrigger;      //外触发 "true", "false", "default" 
        public HTuple CameraType;           //相机类型 "ntsc", "pal", "auto", "default" 
        public HTuple Device;               //设备
        public HTuple Port;                 //端口
        public HTuple LineIn;               //多路复用器的摄像机输入线

        public HTuple Parameters;           //读写参数
        public HTuple ParametersReadonly;   //只读参数
        public HTuple ParametersWriteonly;  //只写参数

        public HTuple Defaults;             //open_framegrabber的默认值
        public HTuple General;              //一般信息
        public HTuple InfoBoards;           //相机信息
        public HTuple Revision;             //版本号
    }



    /// <summary>
    /// 打开相机时所需信息参数
    /// </summary>
    public class HCameraInfo
    {
        public HCameraInfo(HInterfaceName name)
        {
            HTuple information, defaults;
            string item = Enum.GetName(typeof(HInterfaceName), name);
            //item = item.Replace("OTNF", "1394").Replace("PP", "++").Replace("_", "-");
            HOperatorSet.InfoFramegrabber(item, "defaults", out information, out defaults);

            Interface = name;
            HorizontalResolution = defaults[0];
            VerticalResolution = defaults[1];
            ImageWidth = defaults[2];
            ImageHeight = defaults[3];
            StartRow = defaults[4];
            StartColumn = defaults[5];
            Field = (FieldMode)Enum.Parse(typeof(FieldMode), defaults[6]);
            BitsPerChannel = defaults[7];
            ColorSpace = (ColorSpaceMode)Enum.Parse(typeof(ColorSpaceMode), defaults[8]);
            Generic = defaults[9];
            ExternalTrigger = (ExternalTriggerMode)Enum.Parse(typeof(ExternalTriggerMode), defaults[10]);
            CameraType = (CameraTypeMode)Enum.Parse(typeof(CameraTypeMode), defaults[11]);
            Device = defaults[12];
            Port = defaults[13];
            LineIn = defaults[14];
        }

        public HInterfaceName Interface { get; set; }             //HALCON图像获取接口名
        public int HorizontalResolution { get; set; }//水平分辨率
        public int VerticalResolution { get; set; }  //垂直分辨率
        public int ImageWidth { get; set; }          //图像宽度
        public int ImageHeight { get; set; }         //图像高度
        public int StartRow { get; set; }            //开始行
        public int StartColumn { get; set; }         //开始列
        public FieldMode Field { get; set; }            //半图像或全图像 "first", "second", "next", "interlaced", "progressive", "default" 
        public int BitsPerChannel { get; set; }      //每个像素的传输位和图像通道的数量
        public ColorSpaceMode ColorSpace { get; set; }       //颜色空间 "gray", "raw", "rgb", "yuv", "default" 
        public double Generic { get; set; }          //通用参数
        public ExternalTriggerMode ExternalTrigger { get; set; }  //外触发 "true", "false", "default" 
        public CameraTypeMode CameraType { get; set; }       //相机类型 "ntsc", "pal", "auto", "default" 
        public string Device { get; set; }           //设备
        public int Port { get; set; }                //端口
        public int LineIn { get; set; }              //多路复用器的摄像机输入线
    }



    [DefaultEvent("ImageGrabbed")]
    public class HalconCamera : Component
    {
        public HInterfaceInfo InterfaceInfo = new HInterfaceInfo(HInterfaceName.File);
        private HInterfaceName interfaceName = HInterfaceName.File;

        [Browsable(true)]
        [DefaultValue(HInterfaceName.File)]
        [Category("打开相机参数")]
        [Description("HALCON接口名称")]
        public HInterfaceName InterfaceName
        {
            get
            {
                return interfaceName;
            }
            set
            {
                interfaceName = value;
                InterfaceInfo = new HInterfaceInfo(value);
            }
        }

        [Browsable(true)]
        [DefaultValue(-1)]
        [Category("打开相机参数")]
        [Description("水平分辨率")]
        public int HorizontalResolution { get; set; } = -1;

        [Browsable(true)]
        [DefaultValue(-1)]
        [Category("打开相机参数")]
        [Description("垂直分辨率")]
        public int VerticalResolution { get; set; } = -1;

        [Browsable(true)]
        [DefaultValue(-1)]
        [Category("打开相机参数")]
        [Description("图像宽度")]
        public int ImageWidth { get; set; } = -1;

        [Browsable(true)]
        [DefaultValue(-1)]
        [Category("打开相机参数")]
        [Description("图像高度")]
        public int ImageHeight { get; set; } = -1;

        [Browsable(true)]
        [DefaultValue(-1)]
        [Category("打开相机参数")]
        [Description("开始行")]
        public int StartRow { get; set; } = -1;

        [Browsable(true)]
        [DefaultValue(-1)]
        [Category("打开相机参数")]
        [Description("开始行")]
        public int StartColumn { get; set; } = -1;

        [Browsable(true)]
        [DefaultValue(FieldMode.@default)]
        [Category("打开相机参数")]
        [Description("半图像或全图像")]
        public FieldMode Field { get; set; } = FieldMode.@default;

        [Browsable(true)]
        [DefaultValue(-1)]
        [Category("打开相机参数")]
        [Description("每个像素的传输位和图像通道的数量")]
        public int BitsPerChannel { get; set; } = -1;

        [Browsable(true)]
        [DefaultValue(ColorSpaceMode.@default)]
        [Category("打开相机参数")]
        [Description("颜色空间")]
        public ColorSpaceMode ColorSpace { get; set; } = ColorSpaceMode.@default;

        [Browsable(true)]
        [DefaultValue(-1.0)]
        [Category("打开相机参数")]
        [Description("通用参数")]
        public double Generic { get; set; } = -1.0;

        [Browsable(true)]
        [DefaultValue(ExternalTriggerMode.@default)]
        [Category("打开相机参数")]
        [Description("外触发")]
        public ExternalTriggerMode ExternalTrigger { get; set; } = ExternalTriggerMode.@default;

        [Browsable(true)]
        [DefaultValue(CameraTypeMode.@default)]
        [Category("打开相机参数")]
        [Description("相机类型")]
        public CameraTypeMode CameraType { get; set; } = CameraTypeMode.@default;

        [Browsable(true)]
        [DefaultValue("default")]
        [Category("打开相机参数")]
        [Description("设备")]
        public string Device { get; set; } = "default";

        [Browsable(true)]
        [DefaultValue(-1)]
        [Category("打开相机参数")]
        [Description("端口")]
        public int Port { get; set; } = -1;

        [Browsable(true)]
        [DefaultValue(-1)]
        [Category("打开相机参数")]
        [Description("多路复用器的摄像机输入线")]
        public int LineIn { get; set; } = -1;


        [Browsable(true)]
        [DefaultValue(-1.0)]
        [Description("最大延迟")]
        public double MaxDelay { get; set; } = -1.0;


        [Browsable(false)]
        [Description("获取相机是否打开")]
        public bool IsOpen { get; private set; }



        private bool isGrabbing;
        private HObject image;
        private HTuple acqHandle;



        /// <summary>
        /// 图像采集完成处理事件
        /// </summary>
        [Browsable(true)]
        [Description("图像采集完成处理事件")]
        public event EventHandler<ImageGrabbedEventArgs> ImageGrabbed;



        public HalconCamera()
        {

        }

        public HalconCamera(HCameraInfo info)
        {
            InterfaceName = info.Interface;
            HorizontalResolution = info.HorizontalResolution;
            VerticalResolution = info.VerticalResolution;
            ImageWidth = info.ImageWidth;
            ImageHeight = info.ImageHeight;
            StartRow = info.ImageHeight;
            StartColumn = info.StartColumn;
            Field = info.Field;
            BitsPerChannel = info.BitsPerChannel;
            ColorSpace = info.ColorSpace;
            Generic = info.Generic;
            ExternalTrigger = info.ExternalTrigger;
            CameraType = info.CameraType;
            Device = info.Device;
            Port = info.Port;
            LineIn = info.LineIn;
        }

        ~HalconCamera()
        {
            Close();
        }



        /// <summary>
        /// 发现相机
        /// </summary>
        public static List<HInterfaceInfo> Find()
        {
            List<HInterfaceInfo> infoList = new List<HInterfaceInfo>();
            string[] names = Enum.GetNames(typeof(HInterfaceName));
            //string[] names = HOperatorSet.GetParamInfo("open_framegrabber", "Name", "values").SArr;

            foreach (var name in names)
            {
                try
                {
                    //string name = item.Replace("OTNF", "1394").Replace("PP", "++").Replace("_", "-");
                    HInterfaceInfo info = new HInterfaceInfo();
                    HTuple information;
                    HOperatorSet.InfoFramegrabber(name, "info_boards", out information, out info.InfoBoards);

                    if (info.InfoBoards.Length > 0)
                    {
                        info.InterfaceName = (HInterfaceName)Enum.Parse(typeof(HInterfaceName), name);

                        HOperatorSet.InfoFramegrabber(name, "horizontal_resolution", out information, out info.HorizontalResolution);
                        HOperatorSet.InfoFramegrabber(name, "vertical_resolution", out information, out info.VerticalResolution);
                        HOperatorSet.InfoFramegrabber(name, "image_width", out information, out info.ImageWidth);
                        HOperatorSet.InfoFramegrabber(name, "image_height", out information, out info.ImageHeight);
                        HOperatorSet.InfoFramegrabber(name, "start_row", out information, out info.StartRow);
                        HOperatorSet.InfoFramegrabber(name, "start_column", out information, out info.StartColumn);
                        HOperatorSet.InfoFramegrabber(name, "field", out information, out info.Field);
                        HOperatorSet.InfoFramegrabber(name, "bits_per_channel", out information, out info.BitsPerChannel);
                        HOperatorSet.InfoFramegrabber(name, "color_space", out information, out info.ColorSpace);
                        HOperatorSet.InfoFramegrabber(name, "generic", out information, out info.Generic);
                        HOperatorSet.InfoFramegrabber(name, "external_trigger", out information, out info.ExternalTrigger);
                        HOperatorSet.InfoFramegrabber(name, "camera_type", out information, out info.CameraType);
                        HOperatorSet.InfoFramegrabber(name, "device", out information, out info.Device);
                        HOperatorSet.InfoFramegrabber(name, "port", out information, out info.Port);
                        HOperatorSet.InfoFramegrabber(name, "line_in", out information, out info.LineIn);

                        HOperatorSet.InfoFramegrabber(name, "parameters", out information, out info.Parameters);
                        HOperatorSet.InfoFramegrabber(name, "parameters_readonly", out information, out info.ParametersReadonly);
                        HOperatorSet.InfoFramegrabber(name, "parameters_writeonly", out information, out info.ParametersWriteonly);

                        HOperatorSet.InfoFramegrabber(name, "defaults", out information, out info.Defaults);
                        HOperatorSet.InfoFramegrabber(name, "general", out information, out info.General);
                        HOperatorSet.InfoFramegrabber(name, "revision", out information, out info.Revision);

                        infoList.Add(info);
                    }
                }
                catch (Exception)
                {

                }
            }

            return infoList;
        }


        /// <summary>
        /// 打开相机
        /// </summary>
        /// <param name="info">相机信息</param>
        /// <returns></returns>
        public bool Open(HCameraInfo info)
        {
            InterfaceName = info.Interface;
            HorizontalResolution = info.HorizontalResolution;
            VerticalResolution = info.VerticalResolution;
            ImageWidth = info.ImageWidth;
            ImageHeight = info.ImageHeight;
            StartRow = info.ImageHeight;
            StartColumn = info.StartColumn;
            Field = info.Field;
            BitsPerChannel = info.BitsPerChannel;
            ColorSpace = info.ColorSpace;
            Generic = info.Generic;
            ExternalTrigger = info.ExternalTrigger;
            CameraType = info.CameraType;
            Device = info.Device;
            Port = info.Port;
            LineIn = info.LineIn;

            return Open();
        }


        /// <summary>
        /// 打开相机
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            try
            {
                HOperatorSet.OpenFramegrabber(
                        Enum.GetName(typeof(HInterfaceName), InterfaceName),
                        HorizontalResolution,
                        VerticalResolution,
                        ImageWidth,
                        ImageHeight,
                        StartRow,
                        StartColumn,
                        Enum.GetName(typeof(FieldMode), Field),
                        BitsPerChannel,
                        Enum.GetName(typeof(ColorSpaceMode), ColorSpace),
                        Generic,
                        Enum.GetName(typeof(ExternalTriggerMode), ExternalTrigger),
                        Enum.GetName(typeof(CameraTypeMode), CameraType),
                        Device,
                        Port,
                        LineIn,
                        out acqHandle);

                IsOpen = true;
            }
            catch (Exception)
            {
                IsOpen = false;
            }

            return IsOpen;
        }


        /// <summary>
        /// 同步采集一帧图像
        /// </summary>
        /// <returns></returns>
        public bool GrabOne()
        {
            try
            {
                image?.Dispose();
                HOperatorSet.GrabImage(out image, acqHandle);
                ImageGrabbed?.Invoke(this, new ImageGrabbedEventArgs(image));
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// 异步连续采集图像
        /// </summary>
        public async void GrabStartAsync()
        {
            if (isGrabbing)
            {
                return;
            }
            else
            {
                isGrabbing = true;
            }

            await Task.Run(() =>
            {
                try
                {
                    HOperatorSet.GrabImageStart(acqHandle, MaxDelay);

                    while (isGrabbing)
                    {
                        image?.Dispose();
                        HOperatorSet.GrabImageAsync(out image, acqHandle, MaxDelay);
                        ImageGrabbed?.Invoke(this, new ImageGrabbedEventArgs(image));
                    }
                }
                catch (Exception)
                {
                    isGrabbing = false;
                }
            });
        }


        /// <summary>
        /// 停止采集
        /// </summary>
        public void GrabStop()
        {
            isGrabbing = false;
        }


        /// <summary>
        /// 关闭相机
        /// </summary>
        public void Close()
        {
            if (IsOpen)
            {
                if (isGrabbing)
                {
                    isGrabbing = false;
                }

                IsOpen = false;

                HOperatorSet.CloseFramegrabber(acqHandle);
            }
        }



        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="param">
        /// 参数后缀：
        /// -----_access：访问权限，'ro', 'wo', 'rw'
        /// -----_category：类别
        /// -----_description：工具提示
        /// -----_displayname：显示名称
        /// -----_range：范围，[min, max, step, default]，可选'auto' or 'manual'
        /// -----_type：类型
        /// -----_values：有效值列表，['enable','disable']
        /// -----_visibility：可见性，'beginner', 'expert', 'guru'
        /// </param>
        /// <returns>值</returns>
        public HTuple GetParam(HTuple param)
        {
            try
            {
                HTuple value;
                HOperatorSet.GetFramegrabberParam(acqHandle, param, out value);
                return value;
            }
            catch (Exception)
            {
                return null;
            }

            #region
            //switch (value.Length)
            //{
            //    case 1:
            //        switch (value.Type)
            //        {
            //            case HTupleType.INTEGER: return value.I;
            //            case HTupleType.LONG: return value.L;
            //            case HTupleType.DOUBLE: return value.D;
            //            case HTupleType.STRING: return value.S;
            //            default: return null;
            //        }
            //    default:
            //        switch (value.Type)
            //        {
            //            case HTupleType.INTEGER: return value.IArr;
            //            case HTupleType.LONG: return value.LArr;
            //            case HTupleType.DOUBLE: return value.DArr;
            //            case HTupleType.STRING: return value.SArr;
            //            case HTupleType.MIXED: return value;
            //            default: return null;
            //        }
            //}
            #endregion
        }


        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="param">参数</param>
        /// <param name="value">值</param>
        public void SetParam(HTuple param, HTuple value)
        {
            try
            {
                HOperatorSet.SetFramegrabberParam(acqHandle, param, value);
            }
            catch (Exception)
            {

            }
        }
    }
}