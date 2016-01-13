using System;

namespace ManahostManager.Utils
{
    public class ManahostUploadFileSystem
    {
        public static String[] imageExtension = { ".png", ".jpeg", ".jpg" };
        public static String[] otherExtension = { ".tp", ".pdf" };

        private static String PUBLIC_UPLOAD_FOLDER = "Public";
        private static String PRIVATE_UPLOAD_FOLDER = "Private";

        static private void CreateFolder(String root, int CurrentClientId)
        {
            String path = root + @"\" + CurrentClientId;
            String pathPrivate = path + @"\" + PRIVATE_UPLOAD_FOLDER;
            String pathPublic = path + @"\" + PUBLIC_UPLOAD_FOLDER;

            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
                System.IO.Directory.CreateDirectory(pathPrivate);
                System.IO.Directory.CreateDirectory(pathPublic);
            }
        }

        static public String GetUploadFolderPath(String root, int IdCurrentClient, Boolean isPrivate)
        {
            string path;

            path = root + @"\" + IdCurrentClient + @"\" + ((isPrivate) ? PRIVATE_UPLOAD_FOLDER : PUBLIC_UPLOAD_FOLDER) + @"\";
            if (!System.IO.Directory.Exists(path))
                CreateFolder(root, IdCurrentClient);
            return path;
        }

        static public String GetUploadedFilePath(String root, int IdCurrentClient, Boolean isPrivate, String FileName)
        {
            return GetUploadFolderPath(root, IdCurrentClient, isPrivate) + FileName;
        }
    }
}