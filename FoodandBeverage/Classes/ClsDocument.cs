using BLL;
using Domain;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using static FoodandBeverage.Interfaces.Interfaces;

namespace FoodandBeverage.Classes
{
    public class ClsDocument
    {
        public void UploadImage(Bitmap bitmap, String fileName, DocumentType documentType)
        {
            try
            {
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = " gp.GTypeSubText1,gp.GTypeSubText2,gp.GTypeSubText3 ";
                dmnSearch.Table = " tblGeneralOptions as gp inner join tblGType as g on gp.GtypeId=g.GTypeId";
                dmnSearch.WhereNdGroupBy = " where g.GType='FTP' and gp.[Status]=2 ";
                BllSearch<DmnGeneralOptions> bllSearch = new BllSearch<DmnGeneralOptions>();
                var ftpCredentials = bllSearch.GetSearchData("prdSearch", dmnSearch);
                NetworkCredential credential = new NetworkCredential(ftpCredentials[0].GTypeSubText2, ftpCredentials[0].GTypeSubText3);
                // NetworkCredential credential = new NetworkCredential("nisarga@ayjsoft.com", "}F9t{q3X-z}j");
                string filePath = string.Empty;
                switch (documentType)
                {
                    case DocumentType.CustomerPhoto:
                        CheckAndCreateDirectory(ftpCredentials[0].GTypeSubText1.ToString(), "CustomerPhoto", credential);
                        filePath = "/CustomerPhoto/" + fileName;
                        break;
                    case DocumentType.EmployeePhoto:
                        CheckAndCreateDirectory(ftpCredentials[0].GTypeSubText1.ToString(), "EmployeePhoto", credential);
                        filePath = "/EmployeePhoto/" + fileName;
                        break;
                    case DocumentType.EmployeeIdentityProof1:
                    case DocumentType.EmployeeIdentityProof2:
                        CheckAndCreateDirectory(ftpCredentials[0].GTypeSubText1.ToString(), "EmployeeProof", credential);
                        filePath = "/EmployeeProof/" + fileName;
                        break;
                    case DocumentType.ItemPhoto:
                        CheckAndCreateDirectory(ftpCredentials[0].GTypeSubText1.ToString(), "ItemPhoto", credential);
                        filePath = "/ItemPhoto/" + fileName;
                        break;
                    case DocumentType.CompanyLogo:
                        CheckAndCreateDirectory(ftpCredentials[0].GTypeSubText1.ToString(), "CompanyLogo", credential);
                        filePath = "/CompanyLogo/" + fileName;
                        break;
                    case DocumentType.BranchLogo:
                        CheckAndCreateDirectory(ftpCredentials[0].GTypeSubText1.ToString(), "BranchLogo", credential);
                        filePath = "/BranchLogo/" + fileName;
                        break;
                    case DocumentType.Game:
                        CheckAndCreateDirectory(ftpCredentials[0].GTypeSubText1.ToString(), "Game", credential);
                        filePath = "/Game/" + fileName;
                        break;
                    case DocumentType.FoodPhoto:
                        CheckAndCreateDirectory(ftpCredentials[0].GTypeSubText1.ToString(), "FoodPhoto", credential);
                        filePath = "/FoodPhoto/" + fileName;
                        break;
                }

                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(ftpCredentials[0].GTypeSubText1.ToString() + filePath);
                // FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create("ftp://ayjsoft.com/byteprinting/" + fileName);
                request.UsePassive = true;
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = credential;
                request.UseBinary = true;
                //request.Timeout = 3000;
                var memory = new MemoryStream();
                bitmap.Save(memory, ImageFormat.Jpeg);
                memory.Position = 0;
                ServicePointManager.DefaultConnectionLimit = 10;
                using (Stream TempStream = request.GetRequestStream())
                {
                    byte[] bytes = memory.ToArray();
                    TempStream.Write(bytes, 0, bytes.Length);
                    TempStream.Flush();
                }
                request.GetResponse();
                request.Abort();
            }
            catch (Exception ex)
            {
                //Messagebox.show(ex.ToString());
            }
        }

        internal bool FileExistOrNot(string v, object interfaces)
        {
            throw new NotImplementedException();
        }

        public Bitmap DownloadImage(String fileName, DocumentType documentType)
        {
            DmnSearch dmnSearch = new DmnSearch();
            dmnSearch.Select = " gp.GTypeSubText1,gp.GTypeSubText2,gp.GTypeSubText3 ";
            dmnSearch.Table = " tblGeneralOptions as gp inner join tblGType as g on gp.GtypeId=g.GTypeId";
            dmnSearch.WhereNdGroupBy = " where g.GType='FTP' and gp.[Status]=2 ";
            BllSearch<DmnGeneralOptions> bllSearch = new BllSearch<DmnGeneralOptions>();
            var ftpCredentials = bllSearch.GetSearchData("prdSearch", dmnSearch);
            string filePath = string.Empty;
            switch (documentType)
            {
                case DocumentType.CustomerPhoto:
                    filePath = "/CustomerPhoto/" + fileName;
                    break;
                case DocumentType.EmployeePhoto:
                    filePath = "/EmployeePhoto/" + fileName;
                    break;
                case DocumentType.EmployeeIdentityProof1:
                case DocumentType.EmployeeIdentityProof2:
                    filePath = "/EmployeeProof/" + fileName;
                    break;
                case DocumentType.ItemPhoto:
                    filePath = "/ItemPhoto/" + fileName;
                    break;
                case DocumentType.CompanyLogo:
                    filePath = "/CompanyLogo/" + fileName;
                    break;
                case DocumentType.BranchLogo:
                    filePath = "/BranchLogo/" + fileName;
                    break;
                case DocumentType.Game:
                    filePath = "/Game/" + fileName;
                    break;
                case DocumentType.ReaderImages:
                    filePath = "/ReaderImages/" + fileName;
                    break;
                case DocumentType.FoodPhoto:
                    filePath = "/FoodPhoto/" + fileName;
                    break;
            }
            var request = (FtpWebRequest)WebRequest.Create(ftpCredentials[0].GTypeSubText1 + filePath);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(ftpCredentials[0].GTypeSubText2, ftpCredentials[0].GTypeSubText3);
            request.UseBinary = true;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            Bitmap bitmap = new Bitmap(responseStream);
            response.Dispose();
            responseStream.Dispose();
            return bitmap;
        }
        public bool FileExistOrNot(String fileName, DocumentType documentType)
        {
            try
            {
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = " gp.GTypeSubText1,gp.GTypeSubText2,gp.GTypeSubText3 ";
                dmnSearch.Table = " tblGeneralOptions as gp inner join tblGType as g on gp.GtypeId=g.GTypeId";
                dmnSearch.WhereNdGroupBy = " where g.GType='FTP' and gp.[Status]=2 ";
                BllSearch<DmnGeneralOptions> bllSearch = new BllSearch<DmnGeneralOptions>();
                var ftpCredentials = bllSearch.GetSearchData("prdSearch", dmnSearch);
                string filePath = string.Empty;
                switch (documentType)
                {
                    case DocumentType.CustomerPhoto:
                        filePath = "/CustomerPhoto/";
                        break;
                    case DocumentType.EmployeePhoto:
                        filePath = "/EmployeePhoto/";
                        break;
                    case DocumentType.EmployeeIdentityProof1:
                    case DocumentType.EmployeeIdentityProof2:
                        filePath = "/EmployeeProof/";
                        break;
                    case DocumentType.ItemPhoto:
                        filePath = "/ItemPhoto/";
                        break;
                    case DocumentType.CompanyLogo:
                        filePath = "/CompanyLogo/";
                        break;
                    case DocumentType.BranchLogo:
                        filePath = "/BranchLogo/";
                        break;
                    case DocumentType.Game:
                        filePath = "/Game/";
                        break;
                    case DocumentType.ReaderImages:
                        filePath = "/ReaderImages/";
                        break;
                    case DocumentType.FoodPhoto:
                        filePath = "/FoodPhoto/";
                        break;
                }
                List<string> imgUrlList = new List<string>();
                var request = (FtpWebRequest)WebRequest.Create(ftpCredentials[0].GTypeSubText1 + filePath);
                 request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                request.Credentials = new NetworkCredential(ftpCredentials[0].GTypeSubText2, ftpCredentials[0].GTypeSubText3);
                request.UseBinary = true;
                request.Timeout = 3000;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                using (Stream listStream = response.GetResponseStream())
                using (StreamReader listReader = new StreamReader(listStream))
                {
                    while (!listReader.EndOfStream)
                    {
                        imgUrlList.Add(listReader.ReadLine());
                    }
                }
                var imgStatus = imgUrlList.Where(x => x.Contains(fileName)).FirstOrDefault();
                string imageName = Convert.ToString(imgStatus);
                if (!string.IsNullOrWhiteSpace(imageName))
                {
                    string[] nameList = Convert.ToString(imageName).Split(' ');
                    if (nameList.Length > 0)
                    {
                        if (string.Compare(nameList[nameList.Length - 1], fileName) == 0)
                            return true;
                        else
                            return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteImage(String fileName, DocumentType documentType)
        {
            DmnSearch dmnSearch = new DmnSearch();
            dmnSearch.Select = " gp.GTypeSubText1,gp.GTypeSubText2,gp.GTypeSubText3 ";
            dmnSearch.Table = " tblGeneralOptions as gp inner join tblGType as g on gp.GtypeId=g.GTypeId";
            dmnSearch.WhereNdGroupBy = " where g.GType='FTP' and gp.[Status]=2 ";
            BllSearch<DmnGeneralOptions> bllSearch = new BllSearch<DmnGeneralOptions>();
            var ftpCredentials = bllSearch.GetSearchData("prdSearch", dmnSearch);
            string filePath = string.Empty;
            switch (documentType)
            {
                case DocumentType.CustomerPhoto:
                    filePath = "/CustomerPhoto/" + fileName;
                    break;
                case DocumentType.EmployeePhoto:
                    filePath = "/EmployeePhoto/" + fileName;
                    break;
                case DocumentType.EmployeeIdentityProof1:
                case DocumentType.EmployeeIdentityProof2:
                    filePath = "/EmployeeProof/" + fileName;
                    break;
                case DocumentType.ItemPhoto:
                    filePath = "/ItemPhoto/" + fileName;
                    break;
                case DocumentType.CompanyLogo:
                    filePath = "/CompanyLogo/" + fileName;
                    break;
                case DocumentType.BranchLogo:
                    filePath = "/BranchLogo/" + fileName;
                    break;
                case DocumentType.Game:
                    filePath = "/Game/" + fileName;
                    break;
                case DocumentType.ReaderImages:
                    filePath = "/ReaderImages/" + fileName;
                    break;
                case DocumentType.FoodPhoto:
                    filePath = "/FoodPhoto/" + fileName;
                    break;
            }
            var request = (FtpWebRequest)WebRequest.Create(ftpCredentials[0].GTypeSubText1 + filePath);
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.Credentials = new NetworkCredential(ftpCredentials[0].GTypeSubText2, ftpCredentials[0].GTypeSubText3);
            request.UseBinary = true;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            if (response.StatusCode == FtpStatusCode.FileActionOK)
            {
                response.Dispose();
                return true;

            }
            else
            {
                response.Dispose();
                return false;
            }
        }
        public void CheckAndCreateDirectory(string rootPath, string folderName, NetworkCredential credentials)
        {
            bool dirStatus = false;
            FtpWebRequest listDirectory = (FtpWebRequest)WebRequest.Create(rootPath);
            listDirectory.Method = WebRequestMethods.Ftp.ListDirectory;
            listDirectory.Credentials = credentials;
            FtpWebResponse response = (FtpWebResponse)listDirectory.GetResponse();
            StreamReader streamReader = new StreamReader(response.GetResponseStream());
            string line = streamReader.ReadLine();
            if (string.IsNullOrEmpty(line))
            {
                WebRequest request = WebRequest.Create(rootPath + "/" + folderName);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = credentials;
                var resp = request.GetResponse();
            }
            else
            {
                while (!string.IsNullOrEmpty(line))
                {
                    if (string.Compare(folderName, line) == 0)
                    {
                        dirStatus = true;
                        break;
                    }
                    line = streamReader.ReadLine();
                }
            }
            if (!dirStatus)
            {
                WebRequest request = WebRequest.Create(rootPath + "/" + folderName);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = credentials;
                var resp = request.GetResponse();
            }

        }

        public bool CopyFile(string loc, string destipath, DocumentType documentType)
        {
            try
            {
                string filePath = "";
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = " gp.GTypeSubText1,gp.GTypeSubText2,gp.GTypeSubText3 ";
                dmnSearch.Table = " tblGeneralOptions as gp inner join tblGType as g on gp.GtypeId=g.GTypeId";
                dmnSearch.WhereNdGroupBy = " where g.GType='FTP' and gp.[Status]=2 ";
                BllSearch<DmnGeneralOptions> bllSearch = new BllSearch<DmnGeneralOptions>();
                var ftpCredentials = bllSearch.GetSearchData("prdSearch", dmnSearch);
                switch (documentType)
                {
                    case DocumentType.CustomerPhoto:
                        filePath = "/CustomerPhoto/" + destipath;
                        break;
                    case DocumentType.EmployeePhoto:
                        filePath = "/EmployeePhoto/" + destipath;
                        break;
                    case DocumentType.EmployeeIdentityProof1:
                    case DocumentType.EmployeeIdentityProof2:
                        filePath = "/EmployeeProof/" + destipath;
                        break;
                    case DocumentType.ItemPhoto:
                        filePath = "/ItemPhoto/" + destipath;
                        break;
                    case DocumentType.CompanyLogo:
                        filePath = "/CompanyLogo/" + destipath;
                        break;
                    case DocumentType.BranchLogo:
                        filePath = "/BranchLogo/" + destipath;
                        break;
                    case DocumentType.Game:
                        filePath = "/Game/" + destipath;
                        break;
                    case DocumentType.ReaderImages:
                        filePath = "/ReaderImages/" + destipath;
                        break;
                    case DocumentType.FoodPhoto:
                        filePath = "/FoodPhoto/" + destipath;
                        break;
                }
                string s = ftpCredentials[0].GTypeSubText1.ToString() + "" + filePath;
                FtpWebRequest ftpClient = (FtpWebRequest)FtpWebRequest.Create(s);
                ftpClient.Credentials = new System.Net.NetworkCredential(ftpCredentials[0].GTypeSubText2.ToString(), ftpCredentials[0].GTypeSubText3.ToString());
                ftpClient.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
                ftpClient.UseBinary = true;
                ftpClient.KeepAlive = true;
                System.IO.FileInfo fi = new System.IO.FileInfo(loc);
                ftpClient.ContentLength = fi.Length;
                byte[] buffer = new byte[4097];
                int bytes = 0;
                int total_bytes = (int)fi.Length;
                System.IO.FileStream fs = fi.OpenRead();
                System.IO.Stream rs = ftpClient.GetRequestStream();
                while (total_bytes > 0)
                {
                    bytes = fs.Read(buffer, 0, buffer.Length);
                    rs.Write(buffer, 0, bytes);
                    total_bytes = total_bytes - bytes;
                }
                //fs.Flush();
                fs.Close();
                rs.Close();
                FtpWebResponse uploadResponse = (FtpWebResponse)ftpClient.GetResponse();
                string value = uploadResponse.StatusDescription;
                uploadResponse.Close();
                return true;
            }
            catch (Exception ex)
            {

                return false;

            }
        }
    }
}
