using System.Configuration;

namespace Net.Common.Configurations
{
    public class ApplicationConfiguration
    {
        private static readonly ApplicationConfiguration instance = new ApplicationConfiguration();

        public static ApplicationConfiguration Instance { get { return instance; } }

        /// <summary>
        /// 공용 - 회원관련
        /// </summary>
        private ApplicationConfiguration()
        {
            FileSeviceDomain = ConfigurationManager.AppSettings["SeviceDomain"] ?? "local.file.makersn.com";
            FileServerHost = string.Format("http://{0}", FileSeviceDomain);
            FileServerUncPath = ConfigurationManager.AppSettings["FileServerUncPath"] ?? @"\\localhost\fileupload";

            ProfileImg = ConfigurationManager.AppSettings["ProfileImg"] ?? "profile/thumb";
            ProfileCover = ConfigurationManager.AppSettings["ProfileCover"] ?? "profile/cover";
        }

        /// <summary>
        /// 
        /// </summary>
        //public string ServiceMode { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string FileSeviceDomain { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string FileServerHost { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string FileServerUncPath { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProfileImg { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProfileCover { get; private set; }


        #region 디자인 Configurations
        /// <summary>
        /// 디자인
        /// </summary>
        public class DesignConfiguration
        {
            private static readonly DesignConfiguration instance = new DesignConfiguration();

            public static DesignConfiguration Instance { get { return instance; } }

            private DesignConfiguration()
            {
                //design
                DesignImgFile = ConfigurationManager.AppSettings["DesignImgFile"] ?? "design/img-files";
                Design3DFile = ConfigurationManager.AppSettings["Design3DFile"] ?? "design/3d-files";
                DesignJsonFile = ConfigurationManager.AppSettings["DesignJsonFile"] ?? "design/json-files";
                BannerImg = ConfigurationManager.AppSettings["BannerImg"] ?? "design/banner";
                MsgImgThumb = ConfigurationManager.AppSettings["MsgImgThumb"] ?? "design/msg-file/thumb";
                MsgImgOrigin = ConfigurationManager.AppSettings["MsgImgOrigin"] ?? "design/msg-file/backup";
            }

            /// <summary>
            /// 
            /// </summary>
            public string DesignImgFile { get; private set; }

            /// <summary>
            /// 
            /// </summary>
            public string Design3DFile { get; private set; }

            /// <summary>
            /// 
            /// </summary>
            public string DesignJsonFile { get; private set; }

            /// <summary>
            /// 
            /// </summary>
            public string BannerImg { get; private set; }

            /// <summary>
            /// 
            /// </summary>
            public string MsgImgThumb { get; private set; }

            /// <summary>
            /// 
            /// </summary>
            public string MsgImgOrigin { get; private set; }
        } 
        #endregion

        #region 프린팅 Configurations
        /// <summary>
        /// 프린팅
        /// </summary>
        public class PrintingConfiguration
        {
            private static readonly PrintingConfiguration instance = new PrintingConfiguration();

            public static PrintingConfiguration Instance { get { return instance; } }

            private PrintingConfiguration()
            {
                //Printing
            }

            /// <summary>
            /// 
            /// </summary>
            public string Attrbute { get; private set; }
        } 
        #endregion

        #region 스토어 Configurations
        /// <summary>
        /// 스토어
        /// </summary>
        public class StoreConfiguration
        {
            private static readonly StoreConfiguration instance = new StoreConfiguration();

            public static StoreConfiguration Instance { get { return instance; } }

            private StoreConfiguration()
            {
                //Store
                StoreImgFile = ConfigurationManager.AppSettings["StoreImgFile"] ?? "store/img-files";
                Store3DFile = ConfigurationManager.AppSettings["Store3DFile"] ?? "store/3d-files";
                StoreJsonFile = ConfigurationManager.AppSettings["StoreJsonFile"] ?? "store/json-files";
                StoreBanner = ConfigurationManager.AppSettings["StoreBanner"] ?? "store/banner";
                MsgImgThumb = ConfigurationManager.AppSettings["MsgImgThumb"] ?? "store/msg-file/thumb";
                MsgImgOrigin = ConfigurationManager.AppSettings["MsgImgOrigin"] ?? "store/msg-file/backup";
            }

            /// <summary>
            /// 
            /// </summary>
            public string StoreImgFile { get; private set; }

            /// <summary>
            /// 
            /// </summary>
            public string Store3DFile { get; private set; }

            /// <summary>
            /// 
            /// </summary>
            public string StoreJsonFile { get; private set; }

            /// <summary>
            /// 
            /// </summary>
            public string StoreBanner { get; private set; }

            /// <summary>
            /// 
            /// </summary>
            public string MsgImgThumb { get; private set; }

            /// <summary>
            /// 
            /// </summary>
            public string MsgImgOrigin { get; private set; }
        } 
        #endregion
    }
}
