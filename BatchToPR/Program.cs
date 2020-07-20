using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Xml;
using System.Configuration;

namespace BatchToPR
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Test repo");
            //BatchtoPR_cls yo = new BatchtoPR_cls();
            //DHCC dhcc_obj = new DHCC();
            //dhcc_obj.ConvertDateFields();
            //dhcc_obj.RemoveMandatoryNullValues();
            //MisbahTesting obj = new MisbahTesting();
            //XMLReader123 docc = new XMLReader123();
            //redofile.rename();
            //docc.DeformXMLtoCSV();
            //CreateMultipleXML yo = new CreateMultipleXML();

            //ID3Tag yo = new ID3Tag();
            //yo.ID3Control();

            //Splitter yo = new Splitter();
            //yo.SplitFileforJS();

            //IDAM yo = new IDAM();
            //yo.run();

            //FileName yo = new FileName();
            //yo.rename_add();

            //GenerateCustomQuery yo = new GenerateCustomQuery();
            //yo.RUN();

            //IDAM idam = new IDAM();
            //idam.run();

            SD_AttachmentDump yo = new SD_AttachmentDump();

            //RanaGetClaimIDs yo = new RanaGetClaimIDs();



        }

        public class SD_AttachmentDump
        {
            public SD_AttachmentDump()
            {
                try
                {

                    string str = ConfigurationManager.AppSettings.Get("username_CCIS");
                    string str1 = ConfigurationManager.AppSettings.Get("password_CCIS");
                    string str2 = ConfigurationManager.AppSettings.Get("database_CCIS");
                    string str3 = ConfigurationManager.AppSettings.Get("IP_Automation");
                    string basedir = ConfigurationManager.AppSettings.Get("basedir");
                    string connection = string.Concat(new string[] { "Data Source=", str3, ";Initial Catalog=", str2, ";User ID=", str, ";Password=", str1, ";Connection Timeout=30;" });
                    bool openonly = Boolean.Parse(ConfigurationManager.AppSettings.Get("openticketsonly"));


                    TicketHistory(basedir, connection, openonly);
                    TicketActions(basedir, connection, openonly);
                    TicketComments(basedir, connection, openonly);
                    Application(basedir, connection);
                    Attachments(basedir, connection, openonly);
                    AttachmentDump(basedir, connection, openonly);
                    LinkedTickets(basedir, connection, openonly);
                    RequesterDetails(basedir, connection);
                    CreateTicket(basedir, connection, openonly);

                    JiraTicket(basedir, connection, openonly);
                    JiraComments(basedir, connection, openonly);

                    ////Obsoleted
                    //TicketHistoryStatuses(basedir, connection, openonly);

                    ////Not Approved
                    //TicketCommentsActual(basedir, connection, openonly);
                    //JiraTicketsId(basedir, connection, openonly);



                }
                catch (Exception exception)
                {
                    logger.Info(exception);
                }
                Console.WriteLine("Complete");
                Console.Read();
            }

            private void ClearSubject()
            {
                try
                {
                    string query = "update TicketInformation set Subject = TI1.TicketNumber  from (select * from TicketInformation TI2 where TI2.Subject is null) as TI1  where TicketInformation.Subject is null and TicketInformation.TicketInformationID = TI1.TicketInformationID";
                }
                catch (Exception exception)
                {
                    logger.Info(exception);
                }
            }
            public void Tester()
            {
                try
                {
                    string str = ConfigurationManager.AppSettings.Get("username_CCIS");
                    string str1 = ConfigurationManager.AppSettings.Get("password_CCIS");
                    string str2 = ConfigurationManager.AppSettings.Get("database_CCIS");
                    string str3 = ConfigurationManager.AppSettings.Get("IP_Automation");
                    string str4 = ConfigurationManager.AppSettings.Get("basedir");
                    string str5 = string.Concat(new string[] { "Data Source=", str3, ";Initial Catalog=", str2, ";User ID=", str, ";Password=", str1, ";Connection Timeout=30;" });
                    string str6 = "\",\"";
                    string str7 = "\"";
                    StringBuilder stringBuilder = new StringBuilder();
                    DataTable dataTable = this.Execute_Query(str5, "select * from TicketHistory where TicketInformationID = 7085");
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            stringBuilder.Append(string.Concat(new string[] { str7, dataTable.Rows[i][0].ToString(), str6, dataTable.Rows[i][1].ToString(), str6, dataTable.Rows[i][2].ToString(), str6, dataTable.Rows[i][3].ToString(), str6, dataTable.Rows[i][4].ToString(), str6, dataTable.Rows[i][5].ToString(), str6, dataTable.Rows[i][6].ToString(), str6, dataTable.Rows[i][7].ToString(), str6, dataTable.Rows[i][8].ToString(), str6, dataTable.Rows[i][9].ToString(), str6, dataTable.Rows[i][10].ToString(), str6, dataTable.Rows[i][11].ToString(), str6, dataTable.Rows[i][12].ToString(), str6, dataTable.Rows[i][13].ToString(), str6, dataTable.Rows[i][14].ToString(), str6, dataTable.Rows[i][15].ToString(), str6, dataTable.Rows[i][16].ToString(), str6, dataTable.Rows[i][17].ToString(), str6, dataTable.Rows[i][18].ToString(), str6, dataTable.Rows[i][19].ToString(), str6, dataTable.Rows[i][20].ToString(), str6, dataTable.Rows[i][21].ToString(), str6, dataTable.Rows[i][22].ToString(), str6, dataTable.Rows[i][23].ToString(), str6, dataTable.Rows[i][24].ToString(), str6, dataTable.Rows[i][25].ToString(), "\n" }));
                        }
                    }
                    using (StreamWriter streamWriter = File.CreateText(string.Concat(str4, "TicketTester.csv")))
                    {
                        streamWriter.Write(stringBuilder.ToString());
                    }
                }
                catch (Exception exception)
                {
                    logger.Info(exception);
                }
            }

            private void Application(string basedir, string connection)
            {
                logger.Info("Dumping Applications");
                try
                {

                    SQLWriter yo = new SQLWriter();
                    yo.run(basedir, "Applications.csv", Execute_Query(connection, "select ApplicationsID,Name,URL from Applications"));

                    //StringBuilder stringBuilder = new StringBuilder();
                    //string str6 = "ApplicationID,Name,URL\n";
                    //string str7 = "\",\"";
                    //string str8 = "\"";
                    //stringBuilder.Append(str6);
                    //DataTable dataTable = this.Execute_Query(str5, "select ApplicationsID,Name,URL from Applications");
                    //if (dataTable != null && dataTable.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < dataTable.Rows.Count; i++)
                    //    {
                    //        stringBuilder.Append(string.Concat(new string[] { str8, dataTable.Rows[i][0].ToString(), str7, dataTable.Rows[i][1].ToString(), str7, dataTable.Rows[i][2].ToString(), "\"\n" }));
                    //    }
                    //}
                    //using (StreamWriter streamWriter = File.CreateText(string.Concat(str4, "Applications.csv")))
                    //{
                    //    streamWriter.Write(stringBuilder.ToString());
                    //}
                }
                catch (Exception exception)
                {
                    logger.Info(exception);
                }
            }
            private void Attachments(string basedir, string connection, bool openOnly)
            {
                logger.Info("Dumping Attachment");
                try
                {
                    string query = string.Empty;
                    if (!openOnly)
                    {
                        query = "select  ticketinformationid as 'ticket_id',CONVERT(nvarchar,TicketAttachmentID)+'_'+[filename] as 'attachment_fileName',DATEDIFF(s, '1970-01-01 00:00:00', CreationDate) as 'CreationDate'  from ticketattachment with(nolock) where TicketAttachment.TicketInformationID in (select TicketInformationID from TicketHistory th_OPEN with(nolock) where th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock)  where TH3.CreationDate>'2020-06-26 00:00:00' group by TH3.TicketInformationID))";
                    }
                    else
                    {
                        query = "select  ticketinformationid as 'ticket_id',CONVERT(nvarchar,TicketAttachmentID)+'_'+[filename] as 'attachment_fileName',DATEDIFF(s, '1970-01-01 00:00:00', CreationDate) as 'CreationDate'  from ticketattachment with(nolock)  where TicketAttachment.TicketInformationID in (select TicketInformationID from TicketHistory th_OPEN with(nolock) where th_OPEN.IncidentStatusID not in (12,14,39)and th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock) group by TH3.TicketInformationID))";
                    }
                    SQLWriter yo = new SQLWriter();
                    yo.run(basedir, "Attachment.csv", Execute_Query(connection, query));


                    //string str = ConfigurationManager.AppSettings.Get("username_CCIS");
                    //string str1 = ConfigurationManager.AppSettings.Get("password_CCIS");
                    //string str2 = ConfigurationManager.AppSettings.Get("database_CCIS");
                    //string str3 = ConfigurationManager.AppSettings.Get("IP_Automation");
                    //string str4 = ConfigurationManager.AppSettings.Get("basedir");
                    //string str5 = string.Concat(new string[] { "Data Source=", str3, ";Initial Catalog=", str2, ";User ID=", str, ";Password=", str1, ";Connection Timeout=30;" });
                    //StringBuilder stringBuilder = new StringBuilder();
                    //string str6 = "ticket_id,attachment_fileName,CreationDate\n";
                    //string str7 = ConfigurationManager.AppSettings.Get("openticketsonly");
                    //string empty = string.Empty;
                    //empty = (str7 != "true" ? "select  ticketinformationid as 'ticket_id',[filename] as 'filename',DATEDIFF(s, '1970-01-01 00:00:00', CreationDate)   from ticketattachment with(nolock) where TicketAttachment.TicketInformationID in (select TicketInformationID from TicketHistory th_OPEN with(nolock) where th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock) group by TH3.TicketInformationID))" : "select  ticketinformationid as 'ticket_id',[filename] as 'filename',DATEDIFF(s, '1970-01-01 00:00:00', CreationDate)   from ticketattachment with(nolock)  where TicketAttachment.TicketInformationID in (select TicketInformationID from TicketHistory th_OPEN with(nolock) where th_OPEN.IncidentStatusID not in (  12,14,39)and th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock) group by TH3.TicketInformationID))");
                    //string str8 = "\",\"";
                    //stringBuilder.Append(str6);
                    //DataTable dataTable = this.Execute_Query(str5, empty);
                    //if (dataTable != null && dataTable.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < dataTable.Rows.Count; i++)
                    //    {
                    //        stringBuilder.Append(string.Concat(new string[] { dataTable.Rows[i][0].ToString(), str8, dataTable.Rows[i][1].ToString(), str8, dataTable.Rows[i][2].ToString(), "\n" }));
                    //    }
                    //}
                    //using (StreamWriter streamWriter = File.CreateText(string.Concat(str4, "Attachment.csv")))
                    //{
                    //    streamWriter.Write(stringBuilder.ToString());
                    //}
                }
                catch (Exception exception)
                {
                    logger.Info(exception);
                }
            }
            private void AttachmentDump(string basedir, string connection, bool openOnly)
            {
                logger.Info("Dumping RAW attachment");
                try
                {
                    //string str = ConfigurationManager.AppSettings.Get("username_CCIS");
                    //string str1 = ConfigurationManager.AppSettings.Get("password_CCIS");
                    //string str2 = ConfigurationManager.AppSettings.Get("database_CCIS");
                    //string str3 = ConfigurationManager.AppSettings.Get("IP_Automation");
                    //string str4 = string.Concat(new string[] { "Data Source=", str3, ";Initial Catalog=", str2, ";User ID=", str, ";Password=", str1, ";Connection Timeout=30;" });
                    //string str5 = ConfigurationManager.AppSettings.Get("openticketsonly");
                    //string empty = string.Empty;
                    //empty = (str5 != "true" ? "" : "");

                    string query = string.Empty;
                    if (openOnly)
                    {
                        query = "select TicketInformationID,CONVERT(nvarchar,TicketAttachmentID)+'_'+[filename] as 'filename',Attachment from ticketattachment with(nolock) where TicketAttachment.TicketInformationID in (select TicketInformationID from TicketHistory th_OPEN with(nolock) where th_OPEN.IncidentStatusID not in (12,14,39) and th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock) group by TH3.TicketInformationID))";
                    }
                    else
                    {
                        query = "select TicketInformationID,CONVERT(nvarchar,TicketAttachmentID)+'_'+[filename] as 'filename',Attachment from ticketattachment with(nolock) where TicketAttachment.TicketInformationID in (select TicketInformationID from TicketHistory th_OPEN with(nolock) where th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock)  where TH3.CreationDate>'2020-06-26 00:00:00' group by TH3.TicketInformationID))";
                    }


                    DataTable dataTable = Execute_Query(connection, query);
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            byte[] item = (byte[])dataTable.Rows[i][2];
                            string str6 = dataTable.Rows[i][1].ToString();
                            Writefiles(item, str6);
                        }
                    }
                }
                catch (Exception exception)
                {
                    logger.Info(exception);
                }
            }
            private void CreateTicket(string basedir, string connection, bool openOnly)
            {
                logger.Info("Dumping CreateTicket");
                try
                {
                    //string str6 = "TicketId,Title,Description,ActionTaken,CallerId,CalerName,CallerEmail,request_type,support_type,ApplicationName,client_priority,internal_priority,client_severity,internal_severity,assigned_group,raised_byRole,raised_byName,Knowledgebase,created_on,updated_on,TicketNumer,TicketStatusID,TicketStatusDescription,group_name,isinternal,reopened_on,l1_assignee,l2_assignee,l3_assignee,l1_assignee_name,l2_assignee_name,l3_assignee_name,l1_assigneddate,l2_assigneddate,l3_assigneddate,resolution\n";
                    string query = string.Empty;
                    if (openOnly)
                    {
                        query = "select " +
                            "TIN.TicketInformationID as 'TicketId'," +
                            "REPLACE(REPLACE(REPLACE(REPLACE(TIN.Subject, CHAR(13), ''), CHAR(10), ''),',',';'),'\"','') as 'Title'," +
                            "REPLACE(REPLACE(REPLACE(REPLACE(TIN.Description, CHAR(13), ''), CHAR(10), ''),',',';'),'\"','') as 'TicketDescription'," +
                            "REPLACE(REPLACE(REPLACE(REPLACE(TIN.ActionTaken, CHAR(13), ''), CHAR(10), ''),',',';'),'\"','') as 'TicketActionTaken'," +
                            "TIN.CallerKeyID as 'CallerId'," +
                            "REPLACE(REPLACE(REPLACE(REPLACE(CIN.Name, CHAR(13), ''), CHAR(10), ''),',',';'),'\"','') as 'CalerName'," +
                            "CIN.Email as 'CallerEmail'," +
                            "stat.Description as 'request_type'," +
                            "ITST.Description as 'support_type'," +
                            "Apps.Name as 'ApplicationName'," +
                            "ITCP.Description as 'client_priority'," +
                            "ITIP.Description as 'internal_priority'," +
                            "ITCS.Description as 'client_severity'," +
                            "ITIS.Description as 'internal_severity'," +
                            "stat_SIC.Description as 'assigned_group'," +
                            "Roles.Description as 'raised_byRole'," +
                            "TIN.CreatedBy as 'raised_byName'," +
                            "('Steps: '+ REPLACE(REPLACE(REPLACE(Resolution.Steps, CHAR(13), ''), CHAR(10), ''),',',';') +'Cause: '+ REPLACE(REPLACE(REPLACE(Resolution.RootCause, CHAR(13), ''), CHAR(10), ''),',',';')) as 'Knowledgebase'," +
                            "DATEDIFF(s, '1970-01-01 00:00:00', TIN.CreationDate) as 'created_on'," +
                            "DATEDIFF(s, '1970-01-01 00:00:00', TIN.UpdateDate) as 'updated_on'," +
                            "TIN.TicketNumber as 'TicketNumer'," +
                            "Thist.IncidentStatusID as 'TicketStatusID'," +
                            "stat_IS.Description  as 'TicketStatusDescription'," +
                            "'' as 'group_name','' as 'isinternal','' as 'reopened_on','' as 'l1_assignee','' as 'l2_assignee','' as 'l3_assignee','' as 'l1_assignee_name','' as 'l2_assignee_name','' as 'l3_assignee_name','' as 'l1_assigneddate','' as 'l2_assigneddate','' as 'l3_assigneddate','' as 'resolution'"+
                            "from TicketInformation TIN with(nolock) inner join CallerInformation CIN with(nolock) on CIN.CallerInformationID = TIN.callerkeyid  inner join Statuses stat with(nolock)  on stat.StatusesID = TIN.TicketType  inner join TicketHistory Thist with(nolock) on TIN.TicketInformationID = Thist.TicketInformationID and Thist.TicketHistoryID = (select max(Th2.TicketHistoryID) from TicketHistory Th2 with(nolock)  where th2.TicketInformationID = TIN.TicketInformationID)  left join ItemTypes ITST with(nolock) on ITST.itemtypesid = Thist.SupportTypeID  left join Applications Apps with(nolock) on TIN.applicationId  = Apps.applicationsid  left join ItemTypes ITCP with(nolock) on TIN.PriorityID = ITCP.ItemTypesID  left join ItemTypes ITIP with(nolock) on Thist.PriorityID = ITIP.ItemTypesID  left join ItemTypes ITCS with(nolock) on TIN.SeverityID = ITCS.ItemTypesID  left join ItemTypes ITIS with(nolock) on Thist.SeverityID = ITIS.ItemTypesID  left join Statuses stat_SIC with(nolock) on Thist.IncidentSubStatusID = stat_SIC.StatusesID left join Statuses stat_IS with(nolock) on Thist.IncidentStatusID = stat_IS.StatusesID left join PersonInformation PIN with(nolock) on TIN.CreatedBy = PIN.FullName  left join UserRoles UR with(nolock) on UR.PersonID = PIN.PersonInformationID  left join Roles with(nolock) on UR.RoleID = Roles.RolesID  left join Resolution with(nolock) on TIN.TicketInformationID = Resolution.TicketInformationID    where   Thist.TicketInformationID in (select TicketInformationID from TicketHistory th_OPEN  with(nolock) where th_OPEN.IncidentStatusID not in (12,14,39)  and th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock)  where TH3.CreationDate>'2020-06-26 00:00:00' group by TH3.TicketInformationID))  order by TIN.CreationDate asc  ";
                    }
                    else
                    {
                        query = "select " +
                            "TIN.TicketInformationID as 'TicketId'," +
                            "REPLACE(REPLACE(REPLACE(REPLACE(TIN.Subject, CHAR(13), ''), CHAR(10), ''),',',';'),'\"','') as 'Title'," +
                            "REPLACE(REPLACE(REPLACE(REPLACE(TIN.Description, CHAR(13), ''), CHAR(10), ''),',',';'),'\"','') as 'TicketDescription'," +
                            "REPLACE(REPLACE(REPLACE(REPLACE(TIN.ActionTaken, CHAR(13), ''), CHAR(10), ''),',',';'),'\"','') as 'TicketActionTaken'," +
                            "TIN.CallerKeyID as 'CallerId'," +
                            "REPLACE(REPLACE(REPLACE(REPLACE(CIN.Name, CHAR(13), ''), CHAR(10), ''),',',';'),'\"','') as 'CalerName'," +
                            "CIN.Email as 'CallerEmail'," +
                            "stat.Description as 'request_type'," +
                            "ITST.Description as 'support_type'," +
                            "Apps.Name as 'ApplicationName'," +
                            "ITCP.Description as 'client_priority'," +
                            "ITIP.Description as 'internal_priority'," +
                            "ITCS.Description as 'client_severity'," +
                            "ITIS.Description as 'internal_severity'," +
                            "stat_SIC.Description as 'assigned_group'," +
                            "Roles.Description as 'raised_byRole'," +
                            "TIN.CreatedBy as 'raised_byName'," +
                            "('Steps: '+ REPLACE(REPLACE(REPLACE(Resolution.Steps, CHAR(13), ''), CHAR(10), ''),',',';') +'Cause: '+ REPLACE(REPLACE(REPLACE(Resolution.RootCause, CHAR(13), ''), CHAR(10), ''),',',';')) as 'Knowledgebase'," +
                            "DATEDIFF(s, '1970-01-01 00:00:00', TIN.CreationDate) as 'created_on'," +
                            "DATEDIFF(s, '1970-01-01 00:00:00', TIN.UpdateDate) as 'updated_on'," +
                            "TIN.TicketNumber as 'TicketNumer'," +
                            "Thist.IncidentStatusID as 'TicketStatusID'," +
                            "stat_IS.Description  as 'TicketStatusDescription'," +
                            "'' as 'group_name','' as 'isinternal','' as 'reopened_on','' as 'l1_assignee','' as 'l2_assignee','' as 'l3_assignee','' as 'l1_assignee_name','' as 'l2_assignee_name','' as 'l3_assignee_name','' as 'l1_assigneddate','' as 'l2_assigneddate','' as 'l3_assigneddate','' as 'resolution'" +
                            "from TicketInformation TIN with(nolock) inner join CallerInformation CIN with(nolock) on CIN.CallerInformationID = TIN.callerkeyid  inner join Statuses stat with(nolock) on stat.StatusesID = TIN.TicketType  inner join TicketHistory Thist with(nolock) on TIN.TicketInformationID = Thist.TicketInformationID and Thist.TicketHistoryID = (select max(Th2.TicketHistoryID) from TicketHistory Th2 with(nolock) where th2.TicketInformationID = TIN.TicketInformationID)  left join ItemTypes ITST with(nolock)  on ITST.itemtypesid = Thist.SupportTypeID  left join Applications Apps with(nolock) on TIN.applicationId  = Apps.applicationsid  left join ItemTypes ITCP with(nolock) on TIN.PriorityID = ITCP.ItemTypesID  left join ItemTypes ITIP with(nolock) on Thist.PriorityID = ITIP.ItemTypesID  left join ItemTypes ITCS with(nolock) on TIN.SeverityID = ITCS.ItemTypesID  left join ItemTypes ITIS with(nolock) on Thist.SeverityID = ITIS.ItemTypesID  left join Statuses stat_SIC with(nolock) on Thist.IncidentSubStatusID = stat_SIC.StatusesID left join Statuses stat_IS with(nolock) on Thist.IncidentStatusID = stat_IS.StatusesID left join PersonInformation PIN with(nolock) on TIN.CreatedBy = PIN.FullName  left join UserRoles UR with(nolock) on UR.PersonID = PIN.PersonInformationID  left join Roles with(nolock) on UR.RoleID = Roles.RolesID  left join Resolution with(nolock) on TIN.TicketInformationID = Resolution.TicketInformationID    where   Thist.TicketInformationID in (select TicketInformationID from TicketHistory th_OPEN  with(nolock) where th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock)  where TH3.CreationDate>'2020-06-26 00:00:00' group by TH3.TicketInformationID))  order by TIN.CreationDate asc  ";
                    }
                    SQLWriter yo = new SQLWriter();
                    yo.run(basedir, "CreateTicket.csv", Execute_Query(connection, query));

                    //string str = ConfigurationManager.AppSettings.Get("username_CCIS");
                    //string str1 = ConfigurationManager.AppSettings.Get("password_CCIS");
                    //string str2 = ConfigurationManager.AppSettings.Get("database_CCIS");
                    //string str3 = ConfigurationManager.AppSettings.Get("IP_Automation");
                    //string str4 = ConfigurationManager.AppSettings.Get("basedir");
                    //string str5 = string.Concat(new string[] { "Data Source=", str3, ";Initial Catalog=", str2, ";User ID=", str, ";Password=", str1, ";Connection Timeout=30;" });
                    //StringBuilder stringBuilder = new StringBuilder();
                    //string str6 = "TicketId,Title,Description,ActionTaken,CallerId,CalerName,CallerEmail,request_type,support_type,ApplicationName,client_priority,internal_priority,client_severity,internal_severity,assigned_group,raised_byRole,raised_byName,Knowledgebase,created_on,updated_on,TicketNumer,TicketStatusID,TicketStatusDescription,group_name,isinternal,reopened_on,l1_assignee,l2_assignee,l3_assignee,l1_assignee_name,l2_assignee_name,l3_assignee_name,l1_assigneddate,l2_assigneddate,l3_assigneddate,resolution\n";
                    //string str7 = ConfigurationManager.AppSettings.Get("openticketsonly");
                    //string empty = string.Empty;
                    //string str8 = "\"";
                    //empty = (str7 != "true" ? "select TIN.TicketInformationID as 'TicketId',REPLACE(REPLACE(REPLACE(TIN.Subject, CHAR(13), ''), CHAR(10), ''),',',';') as 'Title',REPLACE(REPLACE(REPLACE(TIN.Description, CHAR(13), ''), CHAR(10), ''),',',';') as 'TicketDescription',REPLACE(REPLACE(REPLACE(TIN.ActionTaken, CHAR(13), ''), CHAR(10), ''),',',';') as 'TicketActionTaken',TIN.CallerKeyID as 'CallerId', REPLACE(REPLACE(REPLACE(CIN.Name, CHAR(13), ''), CHAR(10), ''),',',';') as 'CalerName',  CIN.Email as 'CallerEmail',  stat.Description as 'request_type',  ITST.Description as 'support_type',  Apps.Name as 'ApplicationName',  ITCP.Description as 'client_priority',  ITIP.Description as 'internal_priority',  ITCS.Description as 'client_severity',  ITIS.Description as 'internal_severity',  stat_SIC.Description as 'assigned_group',  Roles.Description as 'raised_byRole', TIN.CreatedBy as 'raised_byName',('Steps: '+ REPLACE(REPLACE(REPLACE(Resolution.Steps, CHAR(13), ''), CHAR(10), ''),',',';') +'Cause: '+ REPLACE(REPLACE(REPLACE(Resolution.RootCause, CHAR(13), ''), CHAR(10), ''),',',';')) as 'Knowledgebase',  DATEDIFF(s, '1970-01-01 00:00:00', TIN.CreationDate) as 'created_on',  DATEDIFF(s, '1970-01-01 00:00:00', TIN.UpdateDate) as 'updated_on',TIN.TicketNumber as 'TicketNumer',Thist.IncidentStatusID as 'TicketStatusID',stat_IS.Description  as 'TicketStatusDescription'      from TicketInformation TIN with(nolock) inner join CallerInformation CIN with(nolock) on CIN.CallerInformationID = TIN.callerkeyid  inner join Statuses stat with(nolock) on stat.StatusesID = TIN.TicketType  inner join TicketHistory Thist with(nolock) on TIN.TicketInformationID = Thist.TicketInformationID and Thist.TicketHistoryID = (select max(Th2.TicketHistoryID) from TicketHistory Th2 with(nolock) where th2.TicketInformationID = TIN.TicketInformationID)  left join ItemTypes ITST with(nolock)  on ITST.itemtypesid = Thist.SupportTypeID  left join Applications Apps with(nolock) on TIN.applicationId  = Apps.applicationsid  left join ItemTypes ITCP with(nolock) on TIN.PriorityID = ITCP.ItemTypesID  left join ItemTypes ITIP with(nolock) on Thist.PriorityID = ITIP.ItemTypesID  left join ItemTypes ITCS with(nolock) on TIN.SeverityID = ITCS.ItemTypesID  left join ItemTypes ITIS with(nolock) on Thist.SeverityID = ITIS.ItemTypesID  left join Statuses stat_SIC with(nolock) on Thist.IncidentSubStatusID = stat_SIC.StatusesID left join Statuses stat_IS with(nolock) on Thist.IncidentStatusID = stat_IS.StatusesID left join PersonInformation PIN with(nolock) on TIN.CreatedBy = PIN.FullName  left join UserRoles UR with(nolock) on UR.PersonID = PIN.PersonInformationID  left join Roles with(nolock) on UR.RoleID = Roles.RolesID  left join Resolution with(nolock) on TIN.TicketInformationID = Resolution.TicketInformationID    where   Thist.TicketInformationID in (select TicketInformationID from TicketHistory th_OPEN  with(nolock) where th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock) group by TH3.TicketInformationID))  order by TIN.CreationDate asc  " : "select TIN.TicketInformationID as 'TicketId',REPLACE(REPLACE(REPLACE(TIN.Subject, CHAR(13), ''), CHAR(10), ''),',',';') as 'Title',REPLACE(REPLACE(REPLACE(TIN.Description, CHAR(13), ''), CHAR(10), ''),',',';') as 'TicketDescription',REPLACE(REPLACE(REPLACE(TIN.ActionTaken, CHAR(13), ''), CHAR(10), ''),',',';') as 'TicketActionTaken',TIN.CallerKeyID as 'CallerId', REPLACE(REPLACE(REPLACE(CIN.Name, CHAR(13), ''), CHAR(10), ''),',',';') as 'CalerName',  CIN.Email as 'CallerEmail',  stat.Description as 'request_type',  ITST.Description as 'support_type',  Apps.Name as 'ApplicationName',  ITCP.Description as 'client_priority',  ITIP.Description as 'internal_priority',  ITCS.Description as 'client_severity',  ITIS.Description as 'internal_severity',  stat_SIC.Description as 'assigned_group',  Roles.Description as 'raised_byRole', TIN.CreatedBy as 'raised_byName',('Steps: '+ REPLACE(REPLACE(REPLACE(Resolution.Steps, CHAR(13), ''), CHAR(10), ''),',',';') +'Cause: '+ REPLACE(REPLACE(REPLACE(Resolution.RootCause, CHAR(13), ''), CHAR(10), ''),',',';')) as 'Knowledgebase',  DATEDIFF(s, '1970-01-01 00:00:00', TIN.CreationDate) as 'created_on',  DATEDIFF(s, '1970-01-01 00:00:00', TIN.UpdateDate) as 'updated_on',TIN.TicketNumber as 'TicketNumer',Thist.IncidentStatusID as 'TicketStatusID',stat_IS.Description  as 'TicketStatusDescription'      from TicketInformation TIN with(nolock) inner join CallerInformation CIN with(nolock) on CIN.CallerInformationID = TIN.callerkeyid  inner join Statuses stat with(nolock)  on stat.StatusesID = TIN.TicketType  inner join TicketHistory Thist with(nolock) on TIN.TicketInformationID = Thist.TicketInformationID and Thist.TicketHistoryID = (select max(Th2.TicketHistoryID) from TicketHistory Th2 with(nolock)  where th2.TicketInformationID = TIN.TicketInformationID)  left join ItemTypes ITST with(nolock) on ITST.itemtypesid = Thist.SupportTypeID  left join Applications Apps with(nolock) on TIN.applicationId  = Apps.applicationsid  left join ItemTypes ITCP with(nolock) on TIN.PriorityID = ITCP.ItemTypesID  left join ItemTypes ITIP with(nolock) on Thist.PriorityID = ITIP.ItemTypesID  left join ItemTypes ITCS with(nolock) on TIN.SeverityID = ITCS.ItemTypesID  left join ItemTypes ITIS with(nolock) on Thist.SeverityID = ITIS.ItemTypesID  left join Statuses stat_SIC with(nolock) on Thist.IncidentSubStatusID = stat_SIC.StatusesID left join Statuses stat_IS with(nolock) on Thist.IncidentStatusID = stat_IS.StatusesID left join PersonInformation PIN with(nolock) on TIN.CreatedBy = PIN.FullName  left join UserRoles UR with(nolock) on UR.PersonID = PIN.PersonInformationID  left join Roles with(nolock) on UR.RoleID = Roles.RolesID  left join Resolution with(nolock) on TIN.TicketInformationID = Resolution.TicketInformationID    where   Thist.TicketInformationID in (select TicketInformationID from TicketHistory th_OPEN  with(nolock) where th_OPEN.IncidentStatusID not in (12,14,39)  and th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock) group by TH3.TicketInformationID))  order by TIN.CreationDate asc  ");
                    //string str9 = "\",\"";
                    //stringBuilder.Append(str6);
                    //DataTable dataTable = this.Execute_Query(str5, empty);
                    //if (dataTable != null && dataTable.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < dataTable.Rows.Count; i++)
                    //    {
                    //        stringBuilder.Append(string.Concat(new string[] { str8, dataTable.Rows[i][0].ToString(), str9, dataTable.Rows[i][1].ToString(), str9, dataTable.Rows[i][2].ToString(), str9, dataTable.Rows[i][3].ToString(), str9, dataTable.Rows[i][4].ToString(), str9, dataTable.Rows[i][5].ToString(), str9, dataTable.Rows[i][6].ToString(), str9, dataTable.Rows[i][7].ToString(), str9, dataTable.Rows[i][8].ToString(), str9, dataTable.Rows[i][9].ToString(), str9, dataTable.Rows[i][10].ToString(), str9, dataTable.Rows[i][11].ToString(), str9, dataTable.Rows[i][12].ToString(), str9, dataTable.Rows[i][13].ToString(), str9, dataTable.Rows[i][14].ToString(), str9, dataTable.Rows[i][15].ToString(), str9, dataTable.Rows[i][16].ToString(), str9, dataTable.Rows[i][17].ToString(), str9, dataTable.Rows[i][18].ToString(), str9, dataTable.Rows[i][19].ToString(), str9, dataTable.Rows[i][20].ToString(), str9, dataTable.Rows[i][21].ToString(), str9, dataTable.Rows[i][22].ToString(), "\"\n" }));
                    //    }
                    //}
                    //using (StreamWriter streamWriter = File.CreateText(string.Concat(str4, "CreateTicket.csv")))
                    //{
                    //    streamWriter.Write(stringBuilder.ToString());
                    //}
                }
                catch (Exception exception)
                {
                    logger.Info(exception);
                }
            }
            private void LinkedTickets(string basedir, string connection, bool openOnly)
            {
                logger.Info("Dumping LinkedTickets");
                try
                {

                    string query = string.Empty;
                    if (openOnly)
                    {
                        query = "select TR_TI_ID as 'ticket_id',TR_TI_ToID as 'Linked Ticket'  from TicketRelation with(nolock) where TicketRelation.TR_TI_ID in (select TicketInformationID from TicketHistory th_OPEN with(nolock) where th_OPEN.IncidentStatusID not in (  12,14,39)and th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock)  where TH3.CreationDate>'2020-06-26 00:00:00' group by TH3.TicketInformationID))";
                    }
                    else
                    {
                        query = "select TR_TI_ID as 'ticket_id',TR_TI_ToID as 'Linked Ticket'  from TicketRelation with(nolock) where TicketRelation.TR_TI_ID in (select TicketInformationID from TicketHistory th_OPEN with(nolock) where th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock)  where TH3.CreationDate>'2020-06-26 00:00:00' group by TH3.TicketInformationID))";
                    }
                    SQLWriter yo = new SQLWriter();
                    yo.run(basedir, "LinkedTickets.csv", Execute_Query(connection, query));

                    //string str = ConfigurationManager.AppSettings.Get("username_CCIS");
                    //string str1 = ConfigurationManager.AppSettings.Get("password_CCIS");
                    //string str2 = ConfigurationManager.AppSettings.Get("database_CCIS");
                    //string str3 = ConfigurationManager.AppSettings.Get("IP_Automation");
                    //string str4 = ConfigurationManager.AppSettings.Get("basedir");
                    //string str5 = string.Concat(new string[] { "Data Source=", str3, ";Initial Catalog=", str2, ";User ID=", str, ";Password=", str1, ";Connection Timeout=30;" });
                    //StringBuilder stringBuilder = new StringBuilder();
                    //string str6 = "ticket_id,Linked_Ticket\n";
                    //string str7 = ConfigurationManager.AppSettings.Get("openticketsonly");
                    //string empty = string.Empty;
                    //empty = (str7 != "true" ? "select TR_TI_ID as 'ticket_id',TR_TI_ToID as 'Linked Ticket'  from TicketRelation with(nolock) where TicketRelation.TR_TI_ID in (select TicketInformationID from TicketHistory th_OPEN with(nolock) where th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock) group by TH3.TicketInformationID))" : "select TR_TI_ID as 'ticket_id',TR_TI_ToID as 'Linked Ticket'  from TicketRelation with(nolock) where TicketRelation.TR_TI_ID in (select TicketInformationID from TicketHistory th_OPEN with(nolock) where th_OPEN.IncidentStatusID not in (  12,14,39)and th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock) group by TH3.TicketInformationID))");
                    //string str8 = "\",\"";
                    //stringBuilder.Append(str6);
                    //DataTable dataTable = this.Execute_Query(str5, empty);
                    //if (dataTable != null && dataTable.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < dataTable.Rows.Count; i++)
                    //    {
                    //        stringBuilder.Append(string.Concat(dataTable.Rows[i][0].ToString(), str8, dataTable.Rows[i][1].ToString(), "\n"));
                    //    }
                    //}
                    //using (StreamWriter streamWriter = File.CreateText(string.Concat(str4, "LinkedTickets.csv")))
                    //{
                    //    streamWriter.Write(stringBuilder.ToString());
                    //}
                }
                catch (Exception exception)
                {
                    logger.Info(exception);
                }
            }
            private void RequesterDetails(string basedir, string connection)
            {
                logger.Info("Dumping RequesterDetails");
                try
                {

                    string query = "select CallerInformationID,CallerLicense,Name,Email,PhoneNumber,Location,Department,'NA' as 'DeviceTYpe',MachineName,isOwner,isContactPerson from CallerInformation  where CreationDate>'2020-06-26 00:00:00'";
                    SQLWriter yo = new SQLWriter();
                    yo.run(basedir, "RequesterDetails.csv", Execute_Query(connection, query));

                    //string str = ConfigurationManager.AppSettings.Get("username_CCIS");
                    //string str1 = ConfigurationManager.AppSettings.Get("password_CCIS");
                    //string str2 = ConfigurationManager.AppSettings.Get("database_CCIS");
                    //string str3 = ConfigurationManager.AppSettings.Get("IP_Automation");
                    //string str4 = ConfigurationManager.AppSettings.Get("basedir");
                    //string str5 = string.Concat(new string[] { "Data Source=", str3, ";Initial Catalog=", str2, ";User ID=", str, ";Password=", str1, ";Connection Timeout=30;" });
                    //StringBuilder stringBuilder = new StringBuilder();
                    //string str6 = "CallerID,CallerLicense,Name,Email,PhoneNumber,Location,Department,DeviceType,isOwner,isContactPerson,CallerLicense\n";
                    //string str7 = "\",\"";
                    //string str8 = "\"";
                    //stringBuilder.Append(str6);
                    //DataTable dataTable = this.Execute_Query(str5, "select CallerInformationID,CallerLicense,Name,Email,PhoneNumber,Location,Department,'NA' as 'DeviceTYpe',MachineName,isOwner,isContactPerson,CallerLicense from CallerInformation");
                    //if (dataTable != null && dataTable.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < dataTable.Rows.Count; i++)
                    //    {
                    //        stringBuilder.Append(string.Concat(new string[] { str8, dataTable.Rows[i][0].ToString(), str7, dataTable.Rows[i][1].ToString(), str7, dataTable.Rows[i][2].ToString(), str7, dataTable.Rows[i][3].ToString(), str7, dataTable.Rows[i][4].ToString(), str7, dataTable.Rows[i][5].ToString(), str7, dataTable.Rows[i][6].ToString(), str7, dataTable.Rows[i][7].ToString(), str7, dataTable.Rows[i][8].ToString(), str7, dataTable.Rows[i][9].ToString(), str7, dataTable.Rows[i][10].ToString(), "\"\n" }));
                    //    }
                    //}
                    //using (StreamWriter streamWriter = File.CreateText(string.Concat(str4, "RequesterDetails.csv")))
                    //{
                    //    streamWriter.Write(stringBuilder.ToString());
                    //}
                }
                catch (Exception exception)
                {
                    logger.Info(exception);
                }
            }

            //Falto
            private void TicketActions(string basedir, string connection, bool openOnly)
            {
                logger.Info("Dumping TicketActionss");
                try
                {
                    string query = string.Empty;
                    if (openOnly)
                    {
                        query = "select REPLACE(REPLACE(REPLACE(REPLACE(THis.ActivityComments, CHAR(13), ''), CHAR(10), ''),',',';'),'\"','') as 'ActivityComments',"+
                            "REPLACE(REPLACE(REPLACE(REPLACE(Activity, CHAR(13), ''), CHAR(10), ''),',',';'),'\"','') as 'Activity' ,"+
                            "TicketInformationID,"+
                            "PIn.Email,"+
                            "THis.CreatedBy,"+
                            "DATEDIFF(s, '1970-01-01 00:00:00', THis.CreationDate) as 'CreationDate',"+
                            "DATEDIFF(s, '1970-01-01 00:00:00', THis.UpdateDate) as 'UpdatedDate'"+
                            "from tickethistory THis with(nolock) inner join PersonInformation PIn with(nolock) on PIn.fullname = THis.CreatedBy where THis.TicketInformationID in (select TicketInformationID from TicketHistory th_OPEN with(nolock) where th_OPEN.IncidentStatusID not in (  12,14,39) and th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock)  where TH3.CreationDate>'2020-06-26 00:00:00' group by TH3.TicketInformationID))";
                    }
                    else
                    {
                        query = "select REPLACE(REPLACE(REPLACE(REPLACE(THis.ActivityComments, CHAR(13), ''), CHAR(10), ''),',',';'),'\"','') as 'ActivityComments'," +
                            "REPLACE(REPLACE(REPLACE(REPLACE(Activity, CHAR(13), ''), CHAR(10), ''),',',';'),'\"','') as 'Activity' ," +
                            "TicketInformationID," +
                            "PIn.Email," +
                            "THis.CreatedBy," +
                            "DATEDIFF(s, '1970-01-01 00:00:00', THis.CreationDate) as 'CreationDate'," +
                            "DATEDIFF(s, '1970-01-01 00:00:00', THis.UpdateDate) as 'UpdatedDate'" +
                            "from tickethistory THis with(nolock) inner join PersonInformation PIn with(nolock) on PIn.fullname = THis.CreatedBy where THis.TicketInformationID in (select TicketInformationID from TicketHistory th_OPEN with(nolock) where th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock)  where TH3.CreationDate>'2020-06-26 00:00:00' group by TH3.TicketInformationID))";
                    }
                    SQLWriter yo = new SQLWriter();
                    yo.run(basedir, "TicketActions.csv", Execute_Query(connection, query));

                    //string str = ConfigurationManager.AppSettings.Get("username_CCIS");
                    //string str1 = ConfigurationManager.AppSettings.Get("password_CCIS");
                    //string str2 = ConfigurationManager.AppSettings.Get("database_CCIS");
                    //string str3 = ConfigurationManager.AppSettings.Get("IP_Automation");
                    //string str4 = ConfigurationManager.AppSettings.Get("basedir");
                    //string str5 = string.Concat(new string[] { "Data Source=", str3, ";Initial Catalog=", str2, ";User ID=", str, ";Password=", str1, ";Connection Timeout=30;" });
                    //StringBuilder stringBuilder = new StringBuilder();
                    //string str6 = "comment,type,ticket_id,addedby_email,added_by,created_on,updated_on\n";
                    //string str7 = ConfigurationManager.AppSettings.Get("openticketsonly");
                    //string empty = string.Empty;
                    //string str8 = "\"";
                    //empty = (str7 != "true" ? "select REPLACE(REPLACE(REPLACE(THis.ActivityComments, CHAR(13), ''), CHAR(10), ''),',',';'),REPLACE(REPLACE(REPLACE(Activity, CHAR(13), ''), CHAR(10), ''),',',';'),TicketInformationID,PIn.Email, THis.CreatedBy,DATEDIFF(s, '1970-01-01 00:00:00', THis.CreationDate) as 'CreationDate',DATEDIFF(s, '1970-01-01 00:00:00', THis.UpdateDate) as 'UpdatedDate' from tickethistory THis with(nolock) inner join PersonInformation PIn with(nolock) on PIn.fullname = THis.CreatedBy where THis.TicketInformationID in (select TicketInformationID from TicketHistory th_OPEN with(nolock) where th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock) group by TH3.TicketInformationID))" : "select REPLACE(REPLACE(REPLACE(THis.ActivityComments, CHAR(13), ''), CHAR(10), ''),',',';'),REPLACE(REPLACE(REPLACE(Activity, CHAR(13), ''), CHAR(10), ''),',',';'),TicketInformationID,PIn.Email, THis.CreatedBy,DATEDIFF(s, '1970-01-01 00:00:00', THis.CreationDate) as 'CreationDate',DATEDIFF(s, '1970-01-01 00:00:00', THis.UpdateDate) as 'UpdatedDate' from tickethistory THis with(nolock) inner join PersonInformation PIn with(nolock) on PIn.fullname = THis.CreatedBy where THis.TicketInformationID in (select TicketInformationID from TicketHistory th_OPEN with(nolock) where th_OPEN.IncidentStatusID not in (  12,14,39) and th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock) group by TH3.TicketInformationID))");
                    //string str9 = "\",\"";
                    //stringBuilder.Append(str6);
                    //DataTable dataTable = this.Execute_Query(str5, empty);
                    //if (dataTable != null && dataTable.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < dataTable.Rows.Count; i++)
                    //    {
                    //        stringBuilder.Append(string.Concat(new string[] { str8, dataTable.Rows[i][0].ToString(), str9, dataTable.Rows[i][1].ToString(), str9, dataTable.Rows[i][2].ToString(), str9, dataTable.Rows[i][3].ToString(), str9, dataTable.Rows[i][4].ToString(), str9, dataTable.Rows[i][5].ToString(), str9, dataTable.Rows[i][6].ToString(), "\"\n" }));
                    //    }
                    //}
                    //using (StreamWriter streamWriter = File.CreateText(string.Concat(str4, "TicketActions.csv")))
                    //{
                    //    streamWriter.Write(stringBuilder.ToString());
                    //}
                }
                catch (Exception exception)
                {
                    logger.Info(exception);
                }
            }
            private void TicketComments(string basedir, string connection, bool openOnly)
            {
                logger.Info("Dumping TicketComments");
                try
                {
                    string query = string.Empty;
                    if (openOnly)
                    {
                        query = "select " +
                            "ticketinformationid as 'ticket_id'," +
                            "JTC.JiraTicketCommentsID as 'jira_id'," +
                            "REPLACE(REPLACE(REPLACE(REPLACE(Comments, CHAR(13), ''), CHAR(10), ''),',',';'),'\"','') as 'comment'," +
                            "DATEDIFF(s, '1970-01-01 00:00:00', PIn.CreationDate) as 'created_on'," +
                            "DATEDIFF(s, '1970-01-01 00:00:00', PIn.UpdateDate) as 'updated_on'," +
                            "PIn.Email as 'email'," +
                            "PIn.FullName as 'name'" +
                            "from JiraTicketComments JTC with(nolock) inner join PersonInformation PIn with(nolock) on PIn.fullname = JTC.CreatedBy where JTC.TicketInformationID in (select TicketInformationID from TicketHistory th_OPEN with(nolock) where th_OPEN.IncidentStatusID not in (  12,14,39)and th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock)  where TH3.CreationDate>'2020-06-26 00:00:00'  group by TH3.TicketInformationID))";
                    }
                    else
                    {
                        query = "select " +
                            "ticketinformationid as 'ticket_id'," +
                            "JTC.JiraTicketCommentsID as 'jira_id'," +
                            "REPLACE(REPLACE(REPLACE(REPLACE(Comments, CHAR(13), ''), CHAR(10), ''),',',';'),'\"','') as 'comment'," +
                            "DATEDIFF(s, '1970-01-01 00:00:00', PIn.CreationDate) as 'created_on'," +
                            "DATEDIFF(s, '1970-01-01 00:00:00', PIn.UpdateDate) as 'updated_on'," +
                            "PIn.Email as 'email'," +
                            "PIn.FullName as 'name'" +
                            "from JiraTicketComments JTC with(nolock) inner join PersonInformation PIn with(nolock) on PIn.fullname = JTC.CreatedBy where JTC.TicketInformationID in (select TicketInformationID from TicketHistory th_OPEN with(nolock) where  th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock)  where TH3.CreationDate>'2020-06-26 00:00:00' group by TH3.TicketInformationID))";
                    }
                    SQLWriter yo = new SQLWriter();
                    yo.run(basedir, "TicketComments.csv", Execute_Query(connection, query));

                    //string str = ConfigurationManager.AppSettings.Get("username_CCIS");
                    //string str1 = ConfigurationManager.AppSettings.Get("password_CCIS");
                    //string str2 = ConfigurationManager.AppSettings.Get("database_CCIS");
                    //string str3 = ConfigurationManager.AppSettings.Get("IP_Automation");
                    //string str4 = ConfigurationManager.AppSettings.Get("basedir");
                    //string str5 = string.Concat(new string[] { "Data Source=", str3, ";Initial Catalog=", str2, ";User ID=", str, ";Password=", str1, ";Connection Timeout=30;" });
                    //StringBuilder stringBuilder = new StringBuilder();
                    //string str6 = "ticket_id,jira_id,comment,created_on,updated_on,email,name\n";
                    //string str7 = ConfigurationManager.AppSettings.Get("openticketsonly");
                    //string empty = string.Empty;
                    //empty = (str7 != "true" ? "select ticketinformationid,JTC.JiraTicketCommentsID,REPLACE(REPLACE(REPLACE(Comments, CHAR(13), ''), CHAR(10), ''),',',';'),DATEDIFF(s, '1970-01-01 00:00:00', PIn.CreationDate) as 'CreationDate',DATEDIFF(s, '1970-01-01 00:00:00', PIn.UpdateDate) as 'UpdatedDate',PIn.Email,PIn.FullName from JiraTicketComments JTC with(nolock) inner join PersonInformation PIn with(nolock) on PIn.fullname = JTC.CreatedBy where JTC.TicketInformationID in (select TicketInformationID from TicketHistory th_OPEN with(nolock) where  th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock) group by TH3.TicketInformationID))" : "select ticketinformationid,JTC.JiraTicketCommentsID,REPLACE(REPLACE(REPLACE(Comments, CHAR(13), ''), CHAR(10), ''),',',';'),DATEDIFF(s, '1970-01-01 00:00:00', PIn.CreationDate) as 'CreationDate',DATEDIFF(s, '1970-01-01 00:00:00', PIn.UpdateDate) as 'UpdatedDate',PIn.Email,PIn.FullName from JiraTicketComments JTC with(nolock) inner join PersonInformation PIn with(nolock) on PIn.fullname = JTC.CreatedBy where JTC.TicketInformationID in (select TicketInformationID from TicketHistory th_OPEN with(nolock) where th_OPEN.IncidentStatusID not in (  12,14,39)and th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock)  group by TH3.TicketInformationID))");
                    //string str8 = "\",\"";
                    //string str9 = "\"";
                    //stringBuilder.Append(str6);
                    //DataTable dataTable = this.Execute_Query(str5, empty);
                    //if (dataTable != null && dataTable.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < dataTable.Rows.Count; i++)
                    //    {
                    //        stringBuilder.Append(string.Concat(new string[] { str9, dataTable.Rows[i][0].ToString(), str8, dataTable.Rows[i][1].ToString(), str8, dataTable.Rows[i][2].ToString(), str8, dataTable.Rows[i][3].ToString(), str8, dataTable.Rows[i][4].ToString(), str8, dataTable.Rows[i][5].ToString(), str8, dataTable.Rows[i][6].ToString(), "\"\n" }));
                    //    }
                    //}
                    //using (StreamWriter streamWriter = File.CreateText(string.Concat(str4, "TicketComments.csv")))
                    //{
                    //    streamWriter.Write(stringBuilder.ToString());
                    //}
                }
                catch (Exception exception)
                {
                    logger.Info(exception);
                }
            }
            private void TicketHistory(string basedir, string connection, bool openOnly)
            {
                logger.Info("Dumping TicketHistory");
                try
                {
                    string query = string.Empty;
                    if (openOnly)
                    {
                        query = "select  " +
                            "REPLACE(REPLACE(REPLACE(REPLACE(Comments, CHAR(13), ''), CHAR(10), ''),',',';'),'\"','') as 'comment'," +
                            "TicketInformationID as 'ticket_id'," +
                            "THis.CreatedBy as 'added_by'," +
                            "DATEDIFF(s, '1970-01-01 00:00:00', THis.CreationDate) as 'created_on'," +
                            "THis.UpdatedBy as 'update_by'," +
                            "DATEDIFF(s, '1970-01-01 00:00:00', THis.UpdateDate) as 'UpdatedDate'," +
                            "'' as 'opt_msg'," +
                            "PIn.Email as 'addedby_email'" +
                            "from tickethistory THis with(nolock) inner join PersonInformation PIn with(nolock) on PIn.fullname = THis.CreatedBy where THis.TicketInformationID in (select TicketInformationID from TicketHistory th_OPEN with(nolock) where th_OPEN.IncidentStatusID not in (  12,14,39)and th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock)  where TH3.CreationDate>'2020-06-26 00:00:00' group by TH3.TicketInformationID))";
                    }
                    else
                    {
                        query = "select  " +
                            "REPLACE(REPLACE(REPLACE(REPLACE(Comments, CHAR(13), ''), CHAR(10), ''),',',';'),'\"','') as 'comment'," +
                            "TicketInformationID as 'ticket_id'," +
                            "THis.CreatedBy as 'added_by'," +
                            "DATEDIFF(s, '1970-01-01 00:00:00', THis.CreationDate) as 'created_on'," +
                            "THis.UpdatedBy as 'update_by'," +
                            "DATEDIFF(s, '1970-01-01 00:00:00', THis.UpdateDate) as 'UpdatedDate'," +
                            "'' as 'opt_msg'," +
                            "PIn.Email as 'addedby_email'" +
                            "from tickethistory THis with(nolock) inner join PersonInformation PIn with(nolock) on PIn.fullname = THis.CreatedBy where THis.TicketInformationID in (select TicketInformationID from TicketHistory th_OPEN with(nolock) where th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock)  where TH3.CreationDate>'2020-06-26 00:00:00' group by TH3.TicketInformationID))";
                    }
                    SQLWriter yo = new SQLWriter();
                    yo.run(basedir, "TicketHistory.csv", Execute_Query(connection, query));


                    //string str = ConfigurationManager.AppSettings.Get("username_CCIS");
                    //string str1 = ConfigurationManager.AppSettings.Get("password_CCIS");
                    //string str2 = ConfigurationManager.AppSettings.Get("database_CCIS");
                    //string str3 = ConfigurationManager.AppSettings.Get("IP_Automation");
                    //string str4 = ConfigurationManager.AppSettings.Get("basedir");
                    //string str5 = string.Concat(new string[] { "Data Source=", str3, ";Initial Catalog=", str2, ";User ID=", str, ";Password=", str1, ";Connection Timeout=30;" });
                    //StringBuilder stringBuilder = new StringBuilder();
                    //string str6 = "comment,ticket_id,added_by,created_on,update_by,updated_on,opt_msg,addedby_email\n";
                    //string str7 = ConfigurationManager.AppSettings.Get("openticketsonly");
                    //string empty = string.Empty;
                    //empty = (str7 != "true" ? "select  REPLACE(REPLACE(REPLACE(Comments, CHAR(13), ''), CHAR(10), ''),',',';'),TicketInformationID,THis.CreatedBy,DATEDIFF(s, '1970-01-01 00:00:00', THis.CreationDate) as 'CreationDate',THis.UpdatedBy,DATEDIFF(s, '1970-01-01 00:00:00', THis.UpdateDate) as 'UpdatedDate','' as 'opt_msg',PIn.Email from tickethistory THis with(nolock) inner join PersonInformation PIn with(nolock) on PIn.fullname = THis.CreatedBy where THis.TicketInformationID in (select TicketInformationID from TicketHistory th_OPEN with(nolock) where th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock) group by TH3.TicketInformationID))" : "select  REPLACE(REPLACE(REPLACE(Comments, CHAR(13), ''), CHAR(10), ''),',',';'),TicketInformationID,THis.CreatedBy,DATEDIFF(s, '1970-01-01 00:00:00', THis.CreationDate),THis.UpdatedBy,DATEDIFF(s, '1970-01-01 00:00:00', THis.UpdateDate),''as 'opt_msg',PIn.Email from tickethistory THis with(nolock) inner join PersonInformation PIn with(nolock) on PIn.fullname = THis.CreatedBy where THis.TicketInformationID in (select TicketInformationID from TicketHistory th_OPEN with(nolock) where th_OPEN.IncidentStatusID not in (  12,14,39)and th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock) group by TH3.TicketInformationID))");
                    //string str8 = "\",\"";
                    //string str9 = "\"";
                    //stringBuilder.Append(str6);
                    //DataTable dataTable = this.Execute_Query(str5, empty);
                    //if (dataTable != null && dataTable.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < dataTable.Rows.Count; i++)
                    //    {
                    //        stringBuilder.Append(string.Concat(new string[] { str9, dataTable.Rows[i][0].ToString(), str8, dataTable.Rows[i][1].ToString(), str8, dataTable.Rows[i][2].ToString(), str8, dataTable.Rows[i][3].ToString(), str8, dataTable.Rows[i][4].ToString(), str8, dataTable.Rows[i][5].ToString(), str8, dataTable.Rows[i][6].ToString(), str8, dataTable.Rows[i][7].ToString(), "\"\n" }));
                    //    }
                    //}
                    //using (StreamWriter streamWriter = File.CreateText(string.Concat(str4, "TicketHistory.csv")))
                    //{
                    //    streamWriter.Write(stringBuilder.ToString());
                    //}
                }
                catch (Exception exception)
                {
                    logger.Info(exception);
                }
            }
            private void TicketHistoryStatuses(string basedir, string connection, bool openOnly)
            {
                logger.Info("Dumping TicketHistoryStatuses");
                try
                { 
                    string query = string.Empty;
                    if (openOnly)
                    {
                        query = "select TicketInformationID as 'TicketId',Statuses.Description as 'StatusDescription',Statuses.StatusesID as 'StatusID',TicketHistory.CreationDate as 'CreationDate' from TicketHistory with(nolock) inner join Statuses with(nolock)on Statuses.StatusesID = IncidentStatusID where TicketHistory.IncidentStatusID not in (12,14,39) and TicketHistory.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock)  where TH3.CreationDate>'2020-06-26 00:00:00' group by TH3.TicketInformationID)";
                    }
                    else
                    {
                        query = "select TicketInformationID as 'TicketId',Statuses.Description as 'StatusDescription',Statuses.StatusesID as 'StatusID',TicketHistory.CreationDate as 'CreationDate' from TicketHistory with(nolock) inner join Statuses with(nolock)on Statuses.StatusesID = IncidentStatusID where TicketHistory.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock)  where TH3.CreationDate>'2020-06-26 00:00:00' group by TH3.TicketInformationID)";
                    }
                    SQLWriter yo = new SQLWriter();
                    yo.run(basedir, "TicketHistoryStatus.csv", Execute_Query(connection, query));


                    //string str = ConfigurationManager.AppSettings.Get("username_CCIS");
                    //string str1 = ConfigurationManager.AppSettings.Get("password_CCIS");
                    //string str2 = ConfigurationManager.AppSettings.Get("database_CCIS");
                    //string str3 = ConfigurationManager.AppSettings.Get("IP_Automation");
                    //string str4 = ConfigurationManager.AppSettings.Get("basedir");
                    //string str5 = string.Concat(new string[] { "Data Source=", str3, ";Initial Catalog=", str2, ";User ID=", str, ";Password=", str1, ";Connection Timeout=30;" });
                    //StringBuilder stringBuilder = new StringBuilder();
                    //string str6 = ConfigurationManager.AppSettings.Get("openticketsonly");
                    //string str7 = "TicketId,StatusDescription,StatusID,CreationDate\n";
                    //string str8 = "\",\"";
                    //string str9 = "\"";
                    //stringBuilder.Append(str7);
                    //string empty = string.Empty;
                    //empty = (str6 != "true" ? "select TicketInformationID,Statuses.Description,Statuses.StatusesID,TicketHistory.CreationDate from TicketHistory with(nolock) inner join Statuses with(nolock)on Statuses.StatusesID = IncidentStatusID where TicketHistory.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock) group by TH3.TicketInformationID)" : "select TicketInformationID,Statuses.Description,Statuses.StatusesID,TicketHistory.CreationDate from TicketHistory with(nolock) inner join Statuses with(nolock)on Statuses.StatusesID = IncidentStatusID where TicketHistory.IncidentStatusID not in (12,14,39) and TicketHistory.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock) group by TH3.TicketInformationID)");
                    //DataTable dataTable = this.Execute_Query(str5, empty);
                    //if (dataTable != null && dataTable.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < dataTable.Rows.Count; i++)
                    //    {
                    //        stringBuilder.Append(string.Concat(new string[] { str9, dataTable.Rows[i][0].ToString(), str8, dataTable.Rows[i][1].ToString(), str8, dataTable.Rows[i][2].ToString(), str8, dataTable.Rows[i][3].ToString(), "\"\n" }));
                    //    }
                    //}
                    //using (StreamWriter streamWriter = File.CreateText(string.Concat(str4, "TicketHistoryStatus.csv")))
                    //{
                    //    streamWriter.Write(stringBuilder.ToString());
                    //}
                }
                catch (Exception exception)
                {
                    logger.Info(exception);
                }
            }
            //Falto

            //REAL shit

            private void JiraTicket(string basedir, string connection, bool openOnly)
            {
                logger.Info("Dumping JiraTicket");
                try
                {
                    string query = string.Empty;
                    if (openOnly)
                    {
                        query = "select JT.ticketinformationid as 'ticket_id'," +
                                "JT.JiraTicketID as 'jira_project_id'," +
                                "JT.JiraTicketKey as 'jira_number'," +
                                "JT.Status as 'jira_status'," +
                                "PIn.email as 'assignee_email'," +
                                "JT.CreatedBy as 'reporter', " +
                                "'' as 'issue_type'," +
                                "'' as 'due_date'," +
                                "DATEDIFF(s, '1970-01-01 00:00:00', JT.CreationDate) as 'created_on'," +
                                "DATEDIFF(s, '1970-01-01 00:00:00', JT.UpdateDate) as 'updated_on'" +
                                "from JiraTicket JT with(nolock) " +
                                "inner join PersonInformation PIn with(nolock) on PIn.fullname = JT.CreatedBy " +
                                "where JT.TicketInformationID in (select TicketInformationID from TicketHistory th_OPEN with(nolock) where th_OPEN.IncidentStatusID not in (12,14,39) and th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock)  where TH3.CreationDate>'2020-06-26 00:00:00' group by TH3.TicketInformationID))";
                    }
                    else
                    {
                        query = "select JT.ticketinformationid as 'ticket_id'," +
                                "JT.JiraTicketID as 'jira_project_id'," +
                                "JT.JiraTicketKey as 'jira_number'," +
                                "JT.Status as 'jira_status'," +
                                "PIn.email as 'assignee_email'," +
                                "JT.CreatedBy as 'reporter', " +
                                "'' as 'issue_type'," +
                                "'' as 'due_date'," +
                                "DATEDIFF(s, '1970-01-01 00:00:00', JT.CreationDate) as 'created_on'," +
                                "DATEDIFF(s, '1970-01-01 00:00:00', JT.UpdateDate) as 'updated_on'" +
                                "from JiraTicket JT with(nolock) " +
                                "inner join PersonInformation PIn with(nolock) on PIn.fullname = JT.CreatedBy " +
                                "where JT.TicketInformationID in (select TicketInformationID from TicketHistory th_OPEN with(nolock) where th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock)  where TH3.CreationDate>'2020-06-26 00:00:00' group by TH3.TicketInformationID))";
                    }
                    SQLWriter yo = new SQLWriter();
                    yo.run(basedir, "JiraTicket.csv", Execute_Query(connection, query));
                }
                catch (Exception ex)
                {
                    logger.Info(ex);
                }
            }
            private void JiraComments(string basedir, string connection, bool openOnly)
            {
                logger.Info("Dumping JiraComments");
                try
                {
                    string query = string.Empty;
                    if (openOnly)
                    {
                        query = "select  " +
                                "    th.TicketInformationID as 'ticket_id', " +
                                "	(select top(1) JTC.JiraTicketKey from Jiraticketcomments JTC with (nolock) where JTC.TicketInformationID = th.TicketInformationID) as 'Jira_Number'," +
                                "	th.TicketHistoryID as 'Jira_comment_id'," +
                                "    REPLACE(REPLACE(REPLACE(REPLACE(th.Comments, CHAR(13), ''), CHAR(10), ''), ',', ';'),'\"','') as 'Comments', " +
                                "    DATEDIFF(s, '1970-01-01 00:00:00', th.CreationDate) as 'created_on'," +
                                "    DATEDIFF(s, '1970-01-01 00:00:00', th.UpdateDate) as 'updated_on'," +
                                "	th.CreatedBy" +
                                "    from TicketHistory th with (nolock)" +
                                "	where " +
                                "	th.ticketinformationid in (select TicketInformationID from TicketHistory th_OPEN with(nolock) where th_OPEN.IncidentStatusID not in (12,14,39) and  th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock)  where TH3.CreationDate>'2020-06-26 00:00:00' group by TH3.TicketInformationID))" +
                                "	and th.Comments is not null and th.Comments != '' and exists (select jtc.TicketInformationID from JiraTicketComments jtc where th.TicketInformationID = jtc.TicketInformationID)";
                    }
                    else
                    {
                        query = "select  " +
                            "    th.TicketInformationID as 'ticket_id', " +
                            "	(select top(1) JTC.JiraTicketKey from Jiraticketcomments JTC with (nolock) where JTC.TicketInformationID = th.TicketInformationID) as 'Jira_Number'," +
                            "	th.TicketHistoryID as 'Jira_comment_id'," +
                            "    REPLACE(REPLACE(REPLACE(REPLACE(th.Comments, CHAR(13), ''), CHAR(10), ''), ',', ';'),'\"','') as 'Comments', " +
                            "    DATEDIFF(s, '1970-01-01 00:00:00', th.CreationDate) as 'created_on'," +
                            "    DATEDIFF(s, '1970-01-01 00:00:00', th.UpdateDate) as 'updated_on'," +
                            "	th.CreatedBy" +
                            "    from TicketHistory th with (nolock)" +
                            "	where " +
                            "	th.ticketinformationid in (select TicketInformationID from TicketHistory th_OPEN with(nolock) where th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock)  where TH3.CreationDate>'2020-06-26 00:00:00' group by TH3.TicketInformationID))" +
                            "	and th.Comments is not null and th.Comments != '' and exists (select jtc.TicketInformationID from JiraTicketComments jtc where th.TicketInformationID = jtc.TicketInformationID)";
                    }
                    SQLWriter yo = new SQLWriter();
                    yo.run(basedir, "JiraComments.csv", Execute_Query(connection, query));
                }
                catch (Exception ex)
                {
                    logger.Info(ex);
                }
            }

            //Not Approved
            private void TicketCommentsActual(string basedir, string connection, bool openOnly)
            {
                logger.Info("Dumping TicketCommentsActual");
                try
                {
                    string query = string.Empty;
                    if (openOnly)
                    {
                        query = "select " +
                        "TicketInformationID as 'ticket_id'," +
                        "REPLACE(REPLACE(REPLACE(REPLACE(Comments, CHAR(13), ''), CHAR(10), ''), ',', ';'),'\"','') as '[Ticket Comments]'," +
                        "REPLACE(REPLACE(REPLACE(REPLACE(Activity, CHAR(13), ''), CHAR(10), ''), ',', ';'),'\"','') as '[Ticket Activity]',"+
                        "REPLACE(REPLACE(REPLACE(ActivityComments, CHAR(13), ''), CHAR(10), ''), ',', ';') as '[Ticket ActivityDescription]'," +
                        "th.CreatedBy as 'CreatedBy'," +
                        "DATEDIFF(s, '1970-01-01 00:00:00', CreationDate) as 'CreatedDate'" +
                        "from TicketHistory th with(nolock) where th.ticketinformationid in (select TicketInformationID from TicketHistory th_OPEN with(nolock) where th_OPEN.IncidentStatusID not in (12,14,39) and  th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock) group by TH3.TicketInformationID))";
                    }
                    else
                    {
                        query = "select " +
                        "TicketInformationID as 'ticket_id'," +
                         "REPLACE(REPLACE(REPLACE(REPLACE(Comments, CHAR(13), ''), CHAR(10), ''), ',', ';'),'\"','') as '[Ticket Comments]'," +
                        "REPLACE(REPLACE(REPLACE(REPLACE(Activity, CHAR(13), ''), CHAR(10), ''), ',', ';'),'\"','') as '[Ticket Activity]'," +
                        "REPLACE(REPLACE(REPLACE(ActivityComments, CHAR(13), ''), CHAR(10), ''), ',', ';') as '[Ticket ActivityDescription]'," +
                        "th.CreatedBy as 'CreatedBy'," +
                        "DATEDIFF(s, '1970-01-01 00:00:00', CreationDate) as 'CreatedDate'" +
                        "from TicketHistory th with(nolock) where th.ticketinformationid in (select TicketInformationID from TicketHistory th_OPEN with(nolock) where  th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock) group by TH3.TicketInformationID))";
                    }
                    SQLWriter yo = new SQLWriter();
                    yo.run(basedir, "TicketCommentsActual.csv", Execute_Query(connection, query));
                }
                catch (Exception ex)
                {
                    logger.Info(ex);
                }
            }
            private void JiraTicketsId(string basedir, string connection, bool openOnly)
            {
                logger.Info("Dumping JiraTicketInformation");
                try
                {
                    string query = string.Empty;
                    if (openOnly)
                    {
                        query = "select TicketInformationID,JiraTicketKey,Assignee as 'Assigned To (L3)',CreatedBy as 'Created By (L2)',DATEDIFF(s, '1970-01-01 00:00:00', CreationDate) as 'CreatedDate'"+
                            "from JiraTicket JT with(nolock) where JT.TicketInformationID in (select TicketInformationID from TicketHistory th_OPEN with(nolock) where th_OPEN.IncidentStatusID not in (12,14,39) and th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock) group by TH3.TicketInformationID))";
                    }
                    else
                    {
                        query = "select TicketInformationID,JiraTicketKey,Assignee as 'Assigned To (L3)',CreatedBy as 'Created By (L2)',DATEDIFF(s, '1970-01-01 00:00:00', CreationDate) as 'CreatedDate' "+
                            "from JiraTicket JT with(nolock) where JT.TicketInformationID in (select TicketInformationID from TicketHistory th_OPEN with(nolock) where th_OPEN.TicketHistoryID in (select MAX(TicketHistoryID) from IQServiceDesk_Prod..TicketHistory TH3 with(nolock) group by TH3.TicketInformationID))";
                    }
                    SQLWriter yo = new SQLWriter();
                    yo.run(basedir, "JiraTicketId.csv", Execute_Query(connection, query));
                }
                catch (Exception ex)
                {
                    logger.Info(ex);
                }
            }

            private void Writefiles(byte[] Attachment, string filename)
            {
                Console.WriteLine(string.Concat("writing file ", filename));
                try
                {
                    byte[] attachment = Attachment;
                    File.WriteAllBytes(string.Concat(ConfigurationManager.AppSettings.Get("basedir"), "\\Attachment\\", filename), attachment);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
            private DataTable Execute_Query(string connection, string query)
            {
                DataTable dataTable;
                DataTable dataTable1 = new DataTable();
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connection))
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                        {
                            sqlConnection.Open();
                            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                            {
                                sqlDataAdapter.SelectCommand.CommandTimeout = 1800;
                                sqlDataAdapter.Fill(dataTable1);
                                Console.WriteLine("Query executed successfully");
                            }
                            sqlConnection.Close();
                        }
                    }
                    dataTable = dataTable1;
                }
                catch (Exception exception)
                {
                    logger.Info(exception);
                    dataTable = null;
                }
                return dataTable;
            }
        }

        public static string ConvertDate(string Rdate)
        {
            string str;
            string[] strArrays = new string[] { "M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt", "MM/dd/yyyy hh:mm:ss", "M/d/yyyy h:mm:ss", "M/d/yyyy hh:mm tt", "M/d/yyyy hh tt", "M/d/yyyy h:mm", "M/d/yyyy h:mm", "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm", "dd/MM/yyyy hh:mm:ss", "dd/MM/yyyy HH:mm:ss", "dd-MM-yyyy", "dd-MM-yyyy HH:mm:ss", "dd/MM/yyyy hh:mm:ss tt", "dd/MM/yyyy HH:mm:ss tt" };
            Console.WriteLine(string.Concat("Transforming date ", Rdate));
            string empty = string.Empty;
            bool flag = false;
            DateTime now = DateTime.Now;
            try
            {
                if (!DateTime.TryParseExact(Rdate, strArrays, new CultureInfo("en-US"), DateTimeStyles.None, out now))
                {
                    Console.WriteLine("Unable to convert '{0}' to a date.", Rdate);
                }
                else
                {
                    Console.WriteLine("Converted '{0}' to {1}.", Rdate, now);
                    flag = true;
                }
                if (!flag)
                {
                    DateTime dateTime = DateTime.Now;
                    DateTime dateTime1 = (DateTime.TryParse(Rdate, out dateTime) ? DateTime.Parse(Rdate) : dateTime);
                    if (dateTime != dateTime1)
                    {
                        empty = dateTime1.ToString("MM/dd/yyyy hh:mm:ss");
                    }
                    else
                    {
                        Console.WriteLine(" ~~~~~~~~ not able to parse the date correctly ~~~~~~~~~~~~ ");
                    }
                }
                else
                {
                    empty = now.ToString("MM/dd/yyyy hh:mm:ss");
                }
                str = empty;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                str = null;
            }
            return str;
        }

        public class BatchtoPR_cls
        {
            public BatchtoPR_cls()
            {
            }
            private string ModifyPR_RootNode()
            {
                string str;
                XmlDocument xmlDocument = new XmlDocument();
                int num = 1;
                string[] files = Directory.GetFiles("C:\\Users\\faisal\\Downloads\\SOAP Projects\\PBMSwitch PayerIntegration\\Batch PRs\\", "*.xml");
                try
                {
                    string[] strArrays = files;
                    for (int i = 0; i < (int)strArrays.Length; i++)
                    {
                        string str1 = strArrays[i];
                        xmlDocument.Load(str1);
                        XmlDocument xmlDocument1 = new XmlDocument();
                        XmlElement innerXml = xmlDocument1.CreateElement("Prior.Request");
                        xmlDocument1.AppendChild(innerXml);
                        innerXml.InnerXml = xmlDocument.DocumentElement.InnerXml;
                        string outerXml = xmlDocument1.OuterXml;
                        int num1 = num;
                        num = num1 + 1;
                        using (StreamWriter streamWriter = File.CreateText(string.Format("C:\\Users\\faisal\\Downloads\\SOAP Projects\\PBMSwitch PayerIntegration\\Batch PRs New\\XML_{0}.xml", num1)))
                        {
                            streamWriter.Write(outerXml);
                        }
                        xmlDocument.Save(str1);
                        Console.WriteLine("PR File number {0} created ", num);
                    }
                    str = "Success";
                }
                catch (Exception exception)
                {
                    str = exception.Message.ToString();
                }
                return str;
            }
            public void Run()
            {
                try
                {
                    string str = "C:\\tmp\\BatchtoPrandConfirm\\";
                    string[] files = Directory.GetFiles(str, "*.xml");
                    string empty = string.Empty;
                    string[] strArrays = files;
                    for (int i = 0; i < (int)strArrays.Length; i++)
                    {
                        string str1 = strArrays[i];
                        empty = string.Concat(str, Path.GetFileNameWithoutExtension(str1));
                        Directory.CreateDirectory(empty);
                        int num = 1;
                        XmlDocument xmlDocument = new XmlDocument();
                        xmlDocument.Load(Path.GetFullPath(str1));
                        foreach (XmlNode elementsByTagName in xmlDocument.GetElementsByTagName("requestsList"))
                        {
                            string innerText = elementsByTagName.FirstChild.InnerText;
                            using (StreamWriter streamWriter = File.CreateText(string.Concat(empty, "\\", innerText)))
                            {
                                XmlDocument xmlDocument1 = new XmlDocument();
                                XmlElement innerXml = xmlDocument1.CreateElement("Prior.Request");
                                xmlDocument1.AppendChild(innerXml);
                                innerXml.InnerXml = elementsByTagName.LastChild.InnerXml;
                                streamWriter.Write(xmlDocument1.OuterXml);
                                int num1 = num;
                                num = num1 + 1;
                                Console.WriteLine("File Created {0}_{1}.xml", innerText, num1);
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        public class CreateMultipleXML
        {
            public CreateMultipleXML()
            {
            }

            public void create()
            {
                Console.Write("Enter file absloute path:");
                string str = Console.ReadLine();
                Console.Write("Enter iteration count:");
                int num = Convert.ToInt32(Console.ReadLine());
                XmlDocument xmlDocument = new XmlDocument();
                try
                {
                    xmlDocument.Load(str);
                    XmlNode xmlNodes = xmlDocument.SelectSingleNode("//Authorization//ID");
                    XmlNode str1 = xmlDocument.SelectSingleNode("//Header//TransactionDate");
                    for (int i = 0; i < num; i++)
                    {
                        object[] innerText = new object[] { xmlNodes.InnerText, "-SA-", null, null, null };
                        DateTime now = DateTime.Now;
                        innerText[2] = now.ToString("yyyyMMddhhmmss");
                        innerText[3] = "-";
                        innerText[4] = i;
                        xmlNodes.InnerText = string.Concat(innerText);
                        now = DateTime.Now;
                        str1.InnerText = now.ToString("dd/MM/yyyy hh:mm");
                        object[] directoryName = new object[] { Path.GetDirectoryName(str), "\\Elig_SS_", null, null, null, null };
                        now = DateTime.Now;
                        directoryName[2] = now.ToString("yyyyMMddhhmmss");
                        directoryName[3] = "_";
                        directoryName[4] = i;
                        directoryName[5] = ".xml";
                        using (StreamWriter streamWriter = File.CreateText(string.Concat(directoryName)))
                        {
                            Console.WriteLine(string.Concat("Writing File ", i));
                            streamWriter.Write(xmlDocument.OuterXml.ToString());
                        }
                    }
                    Console.WriteLine("Press any key to conitnue");
                }
                catch (Exception exception)
                {
                    Console.WriteLine(string.Concat("Error Occured!", exception.Message));
                    Console.WriteLine("Press any key to conitnue");
                    Console.Read();
                }
            }
        }

        public class DHCC
        {
            public DHCC()
            {
            }
            public void ConvertDateFields()
            {
                string[] strArrays = File.ReadAllLines("C:\\Users\\fansari.INTERNAL\\Desktop\\dhcc\\RUKAB\\ToTransform.csv");
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 1; i < (int)strArrays.Length; i++)
                {
                    string[] strArrays1 = strArrays[i].Split(new char[] { '|' });
                    strArrays1[0] = Program.ConvertDate(strArrays1[0].ToString());
                    strArrays1[1] = Program.ConvertDate(strArrays1[1].ToString());
                    strArrays1[9] = Program.ConvertDate(strArrays1[9].ToString());
                    strArrays1[10] = Program.ConvertDate(strArrays1[10].ToString());
                    string empty = string.Empty;
                    for (int j = 0; j < (int)strArrays1.Length; j++)
                    {
                        empty = string.Concat(empty, strArrays1[j].ToString(), "|");
                    }
                    stringBuilder.Append(string.Concat(empty, "\n"));
                }
                using (StreamWriter streamWriter = File.CreateText("C:\\Users\\fansari.INTERNAL\\Desktop\\dhcc\\RUKAB\\ToTransform_2.csv"))
                {
                    streamWriter.WriteLine(stringBuilder);
                }
                Console.WriteLine("Press any key to continue...");
                Console.Read();
            }
            public void RemoveMandatoryNullValues()
            {
                string[] strArrays = File.ReadAllLines("C:\\Users\\fansari.INTERNAL\\Desktop\\dhcc\\techsupport_clinician_transformed.csv");
                StringBuilder stringBuilder = new StringBuilder();
                int num = 0;
                int num1 = 0;
                for (int i = 1; i < (int)strArrays.Length; i++)
                {
                    num++;
                    string empty = string.Empty;
                    string[] strArrays1 = strArrays[i].Split(new char[] { '|' });
                    try
                    {
                        for (int j = 0; j < (int)strArrays1.Length; j++)
                        {
                            num1 = 0;
                            if (strArrays1[2].Trim().Length <= 0 || strArrays1[3].Trim().Length <= 0 || strArrays1[11].Trim().Length <= 0 || strArrays1[12].Trim().Length <= 0 || strArrays1[21].Trim().Length <= 0)
                            {
                                Console.WriteLine(string.Concat("Empty Record Found at iteration: ", num));
                            }
                            else
                            {
                                empty = (!strArrays1[j].Contains(",") ? string.Concat(empty, strArrays1[j].ToString(), "|") : string.Concat(empty, strArrays1[j].Replace(',', ' '), "|"));
                            }
                        }
                        stringBuilder.Append(string.Concat(empty, "\n"));
                    }
                    catch (Exception exception1)
                    {
                        Exception exception = exception1;
                        Console.WriteLine(string.Concat(new object[] { "Exception occured at line:", num, " at row:", num1, "\nError:", exception.Message }));
                        Console.Read();
                    }
                }
                using (StreamWriter streamWriter = File.CreateText("C:\\Users\\fansari.INTERNAL\\Desktop\\dhcc\\techsupport_clinician_transformed_mandatory.csv"))
                {
                    streamWriter.WriteLine("Start Date|End Date|Clinician License|Clinician Name|Username|Password|Facility License|Facility Name|Location|Active From|Active To|active|source|Specilaity ID 1|Speciality Description|Gender|Nationality|Email|Phone|Speciality ID 2|Speciality ID 3|type");
                    streamWriter.WriteLine(stringBuilder);
                }
            }
            public void UpdateDate()
            {
                string[] strArrays = File.ReadAllLines("C:\\Users\\fansari.INTERNAL\\Desktop\\dhcc\\techsupport_clinician_20180708165202.csv");
                string[] strArrays1 = File.ReadAllLines("C:\\Users\\fansari.INTERNAL\\Desktop\\dhcc\\techsupport_clinician_Transformed_20180708165255.csv");
                StringBuilder stringBuilder = new StringBuilder();
                StringBuilder stringBuilder1 = new StringBuilder();
                for (int i = 1; i < (int)strArrays.Length; i++)
                {
                    string[] strArrays2 = strArrays[i].Split(new char[] { '|' });
                    string[] strArrays3 = strArrays1[i].Split(new char[] { '|' });
                    string str = Program.ConvertDate(strArrays2[10]);
                    string empty = string.Empty;
                    for (int j = 0; j < (int)strArrays2.Length; j++)
                    {
                        if (j == 10)
                        {
                            empty = string.Concat(empty, str, "|");
                        }
                        else if (j != 10)
                        {
                            empty = string.Concat(empty, strArrays3[j].ToString(), "|");
                        }
                    }
                    stringBuilder1.Append(string.Concat(empty, "\n"));
                }
                using (StreamWriter streamWriter = File.CreateText("C:\\Users\\fansari.INTERNAL\\Desktop\\dhcc\\techsupport_clinician_transformed.csv"))
                {
                    streamWriter.WriteLine(stringBuilder1);
                }
            }
        }

        public class DHPOInsert
        {
            public DHPOInsert()
            {
            }
            public void run()
            {
                try
                {
                    string[] strArrays = File.ReadAllLines("C:\\Users\\Fansari\\Downloads\\_Clinicians__202004281955.csv");
                    StringBuilder stringBuilder = new StringBuilder();
                    string[] strArrays1 = strArrays;
                    for (int i = 0; i < (int)strArrays1.Length; i++)
                    {
                        string str = strArrays1[i];
                        stringBuilder.Append("");
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
        }

        public class FileName
        {
            public FileName()
            {
            }
            public void GetFilename()
            {
                try
                {
                    string[] files = Directory.GetFiles("C:\\Users\\fansari.INTERNAL\\Desktop\\PI_ERROR", "*.xml");
                    StringBuilder stringBuilder = new StringBuilder();
                    string[] strArrays = files;
                    for (int i = 0; i < (int)strArrays.Length; i++)
                    {
                        string str = Path.GetFileNameWithoutExtension(strArrays[i]).Replace("PA_", "");
                        stringBuilder.Append(string.Concat(str, ","));
                        using (StreamWriter streamWriter = File.CreateText("C:\\Users\\fansari.INTERNAL\\Desktop\\PI_ERROR\\Filename.csv"))
                        {
                            streamWriter.Write(stringBuilder);
                        }
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.WriteLine("Press any key to conitnue...");
                }
            }
            public void rename()
            {
                try
                {
                    string str = "*.mp3";
                    int num = 5;
                    string[] files = Directory.GetFiles("C:\\tmp\\ID3Project\\Songs\\", str);
                    for (int i = 0; i < (int)files.Length; i++)
                    {
                        string str1 = files[i];
                        string str2 = Path.GetFileNameWithoutExtension(str1).Remove(0, num);
                        File.Move(str1, string.Concat(Path.GetDirectoryName(str1).ToString(), "\\", str2, Path.GetExtension(str1)));
                    }
                    Console.WriteLine("Please enter any key to conitnue...");
                    Console.Read();
                }
                catch (Exception exception)
                {
                    Console.Write(exception.Message);
                    Console.WriteLine("Please enter any key to conitnue...");
                    Console.Read();
                }
            }
            public void rename_add()
            {
                try
                {
                    string str = "*.mp3";
                    string str1 = "The Assassins Creed Brotherhood - ";
                    string[] files = Directory.GetFiles("C:\\tmp\\ID3Project\\Songs\\", str);
                    for (int i = 0; i < (int)files.Length; i++)
                    {
                        string str2 = files[i];
                        string str3 = string.Concat(str1, Path.GetFileNameWithoutExtension(str2));
                        File.Move(str2, string.Concat(Path.GetDirectoryName(str2).ToString(), "\\", str3, Path.GetExtension(str2)));
                    }
                    Console.WriteLine("Please enter any key to conitnue...");
                    Console.Read();
                }
                catch (Exception exception)
                {
                    Console.Write(exception.Message);
                    Console.WriteLine("Please enter any key to conitnue...");
                    Console.Read();
                }
            }
            public void replace()
            {
                try
                {
                    string[] files = Directory.GetFiles("C:\\tmp\\ID3Project\\Songs\\", "*.mp3");
                    for (int i = 0; i < (int)files.Length; i++)
                    {
                        string str = files[i];
                        string str1 = Path.GetFileNameWithoutExtension(str).Replace("E04S04", "Coke Studio");
                        File.Move(str, string.Concat(Path.GetDirectoryName(str).ToString(), "\\", str1, Path.GetExtension(str)));
                    }
                }
                catch (Exception exception)
                {
                    Console.Write(exception.Message);
                    Console.WriteLine("Please enter any key to conitnue...");
                    Console.Read();
                }
            }
        }

        public class GenerateCustomQuery
        {
            public GenerateCustomQuery()
            {
            }
            public void RUN()
            {
                string empty = string.Empty;
                string str = string.Empty;
                empty = "C:\\Users\\Fansari\\Downloads\\_Clinicians__202004281955.csv";
                str = "INSERT INTO clinicianold (clinicianlicense, username, [password]) VALUES ('@1','@2','@3')";
                try
                {
                    string[] strArrays = File.ReadAllLines(empty);
                    using (StreamWriter streamWriter = File.CreateText("C:\\tmp\\CustomQuery_Result.csv"))
                    {
                        string[] strArrays1 = strArrays;
                        for (int i = 0; i < (int)strArrays1.Length; i++)
                        {
                            string str1 = strArrays1[i];
                            StringBuilder stringBuilder = new StringBuilder();
                            string str2 = str;
                            string[] strArrays2 = str1.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                            for (int j = 1; j < (int)strArrays2.Length + 1; j++)
                            {
                                if (j <= 9)
                                {
                                    str2 = str2.Replace(string.Concat("@", j), strArrays2[j - 1]);
                                }
                                else if (j > 9 && j <= 19)
                                {
                                    str2 = str2.Replace(string.Concat("#", j), strArrays2[j - 1]);
                                }
                                else if (j > 19 && j <= 29)
                                {
                                    str2 = str2.Replace(string.Concat("$", j), strArrays2[j - 1]);
                                }
                                str2 = str2.Replace("'NULL'", "NULL");
                            }
                            stringBuilder.Append("\n");
                            stringBuilder.Append(str2);
                            streamWriter.Write(stringBuilder);
                        }
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.WriteLine("Press any key to continue...");
                    Console.Read();
                }
            }
        }

        public class IDAM
        {
            public IDAM()
            {
                run();
            }
            private byte[] AesEncrypt(byte[] bytes, byte[] key)
            {
                byte[] array;
                if (bytes == null || bytes.Length == 0 || key == null || key.Length == 0)
                {
                    throw new ArgumentNullException();
                }
                byte[] numArray = new byte[] { 64, 36, 38, 7, 122, 35, 43, 47, 105, 125, 115, 40, 92, 72, 25, 37 };
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (RijndaelManaged rijndaelManaged = new RijndaelManaged()
                    {
                        Key = key,
                        IV = numArray,
                        Padding = PaddingMode.PKCS7,
                        Mode = CipherMode.CBC
                    })
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateEncryptor(key, numArray), CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(bytes, 0, (int)bytes.Length);
                        }
                    }
                    array = memoryStream.ToArray();
                }
                return array;
            }
            public string AuthenticateUser(string userName, string Password)
            {
                string str;
                string str1 = "ffffffff-c3a6-1e23-218b-c8c60033c587-DHAAPP";
                string str2 = string.Concat(new string[] { "DeviceId=", str1, "&DeviceType=ANDROID&UserId=", userName, "&AppId=SEHHATI&Password=", Password });
                string empty = string.Empty;
                string base64String = string.Empty;
                string empty1 = string.Empty;
                try
                {
                    byte[] numArray = Convert.FromBase64String("mI5cojV7vCWW4Y63BhV9hA==");
                    Encoding uTF8 = Encoding.UTF8;
                    DateTime now = DateTime.Now;
                    byte[] numArray1 = this.AesEncrypt(uTF8.GetBytes(string.Concat(str1, ":", now.ToString("MM/dd/yyyy HH-mm-ss"))), numArray);
                    if (numArray1 != null)
                    {
                        base64String = Convert.ToBase64String(numArray1);
                        empty1 = Program.IDAM.POST_Call("https://eservicesstg.dha.gov.ae:8080/UserManagementAPI/CoreManagement/AuthenticateUser", str2, base64String);
                        JObject jObjects = JObject.Parse(empty1);
                        string[] strArrays = new string[] { "C:\\tmp\\IDAM\\", null, null, null, null };
                        now = DateTime.Now;
                        strArrays[1] = now.ToString("yyyMMddHHmmss");
                        strArrays[2] = "_";
                        strArrays[3] = userName;
                        strArrays[4] = "_AuthenticateUser.json";
                        using (StreamWriter streamWriter = File.CreateText(string.Concat(strArrays)))
                        {
                            streamWriter.Write(jObjects.ToString());
                        }
                        empty1 = jObjects["IsAuthenticated"].ToString();
                    }
                    str = empty1;
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    Console.WriteLine(string.Concat("Exception Occured: ", exception.Message));
                    Console.Read();
                    str = string.Concat("Failiure Occured: ", exception.Message);
                }
                return str;
            }
            private static string POST_Call(string URL, string postdata, string staticToken)
            {
                HttpWebRequest length = (HttpWebRequest)WebRequest.Create(URL);
                byte[] bytes = Encoding.ASCII.GetBytes(postdata);
                length.Headers.Add(HttpRequestHeader.Authorization, string.Concat("Basic ", staticToken));
                length.Accept = "application/json";
                length.Headers.Add("APIVERSION", "3.0");
                length.Headers.Add("uid", "ffffffff-c3a6-1e23-218b-c8c60033c587-DHAAPP");
                length.Method = "POST";
                length.ContentType = "application/x-www-form-urlencoded";
                length.ContentLength = (long)((int)bytes.Length);
                using (Stream requestStream = length.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, (int)bytes.Length);
                }
                return (new StreamReader(((HttpWebResponse)length.GetResponse()).GetResponseStream())).ReadToEnd();
            }
            public string RetrieveUserDetails(string userName, string Password)
            {
                string str;
                string str1 = "ffffffff-c3a6-1e23-218b-c8c60033c587-DHAAPP";
                string str2 = string.Concat(new string[] { "DeviceId=", str1, "&DeviceType=ANDROID&UserId=", userName, "&AppId=SEHHATI&Password=", Password });
                string empty = string.Empty;
                string base64String = string.Empty;
                string empty1 = string.Empty;
                try
                {
                    byte[] numArray = Convert.FromBase64String("mI5cojV7vCWW4Y63BhV9hA==");
                    Encoding uTF8 = Encoding.UTF8;
                    DateTime now = DateTime.Now;
                    byte[] numArray1 = this.AesEncrypt(uTF8.GetBytes(string.Concat(str1, ":", now.ToString("MM/dd/yyyy HH-mm-ss"))), numArray);
                    if (numArray1 != null)
                    {
                        base64String = Convert.ToBase64String(numArray1);
                        empty1 = Program.IDAM.POST_Call("https://eservicesstg.dha.gov.ae:8080/UserManagementAPI/CoreManagement/AuthenticateUser", str2, base64String);
                        empty1 = JObject.Parse(empty1)["IsAuthenticated"].ToString();
                    }
                    str = empty1;
                }
                catch (Exception exception)
                {
                    str = string.Concat("Failiure Occured: ", exception.Message);
                }
                return str;
            }
            public void run()
            {
                string[] lines = File.ReadAllLines("C:\\tmp\\IDAM\\Input.cs");
                foreach (string line in lines)
                {
                    string[] row = line.Split(',');
                    string username = row[0];
                    string password = row[1];
                    logger.Info("Authenticating username: " + username);
                    AuthenticateUser(username, password);
                }
                //AuthenticateUser("RupaRA", "password");
            }
        }

        public class RanaGetEncounterType
        {
            public RanaGetEncounterType()
            {
            }
            public void run()
            {
                try
                {
                    string[] files = Directory.GetFiles("C:\\Important Files\\Injazat\\Success\\", "*request*.*");
                    for (int i = 0; i < (int)files.Length; i++)
                    {
                        string str = files[i];
                        XmlDocument xmlDocument = new XmlDocument();
                        xmlDocument.Load(str);
                        string innerText = xmlDocument.SelectSingleNode("//Submission//Transaction/Encounter/Type").InnerText;
                        if (innerText == "3" || innerText == "4")
                        {
                            File.Copy(str, string.Concat("c:\\tmp\\RanaEncType\\", Path.GetFileName(str)));
                        }
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
                Console.WriteLine("Program complete");
                Console.Read();
            }
        }

        public class RanaGetClaimIDs
        {
            public RanaGetClaimIDs()
            {

                //SELECT CustomerId
                //FROM(VALUES(18736611), (1111111)) V(CustomerId)
                //EXCEPT
                //SELECT SubmissionID
                //FROM   RemittanceClaims


                string baseDir = @"C:\tmp\ECL-28021\";
                try
                {
                    string[] files = Directory.GetFiles(baseDir, "*.xml");
                    
                    for (int i = 0; i < (int)files.Length; i++)
                    {
                        StringBuilder sb = new StringBuilder();
                        string str = files[i];
                        XmlDocument xmlDocument = new XmlDocument();
                        xmlDocument.Load(str);
                        XmlNodeList Claims  = xmlDocument.SelectNodes("Claim.Submission//Claim");

                        foreach (XmlNode claim in Claims)
                        {
                            sb.Append("('" + claim["ID"].InnerText + "')" + ","); 
                        }

                        using (StreamWriter writer = File.CreateText(baseDir + Path.GetFileNameWithoutExtension(str)+ "_Output.csv"))
                        {
                            writer.Write(sb.ToString());
                        }
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
                Console.WriteLine("Program complete");
            }
        }

        public class Run_Query_1by1
        {
            public Run_Query_1by1()
            {
            }
            public void Exceute()
            {
                try
                {
                    logger.Info("Running Query 1by1");
                    string str = ConfigurationManager.AppSettings.Get("basedir");
                    string str1 = ConfigurationManager.AppSettings.Get("RunQuery1by1_username");
                    string str2 = ConfigurationManager.AppSettings.Get("RunQuery1by1_password");
                    string str3 = ConfigurationManager.AppSettings.Get("RunQuery1by1_db");
                    string str4 = ConfigurationManager.AppSettings.Get("RunQuery1by1_ip");
                    string str5 = string.Concat(new string[] { "Data Source=", str4, ";Initial Catalog=", str3, ";User ID=", str1, ";Password=", str2, ";Connection Timeout=30;" });
                    int num = 0;
                    string str6 = "\",\"";
                    string str7 = "\"";
                    bool flag = false;
                    string[] strArrays = File.ReadAllLines(string.Concat(str, "input.csv"));
                    string str8 = File.ReadAllText(string.Concat(str, "Query.sql"));
                    int length = (int)strArrays.Length;
                    string[] strArrays1 = strArrays;
                    for (int i = 0; i < (int)strArrays1.Length; i++)
                    {
                        string str9 = strArrays1[i];
                        StringBuilder stringBuilder = new StringBuilder();
                        logger.Info(string.Concat("Running query for data: ", str9));
                        string str10 = str8.Replace("@1", str9);
                        DataTable dataTable = this.Execute_Query(str5, str10);
                        if (dataTable != null)
                        {
                            if (dataTable.Rows.Count > 0)
                            {
                                if (!flag)
                                {
                                    logger.Info("Parsing Header");
                                    this.WriteitDown(this.SetHeader(dataTable, str6, str7), str);
                                    flag = true;
                                }
                                for (int j = 0; j < dataTable.Rows.Count; j++)
                                {
                                    logger.Info("Parsing result");
                                    stringBuilder.Append(str7);
                                    for (int k = 0; k < dataTable.Columns.Count; k++)
                                    {
                                        if (k != dataTable.Columns.Count - 1)
                                        {
                                            stringBuilder.Append(string.Concat(dataTable.Rows[j][k].ToString(), str6));
                                        }
                                        else if (k == dataTable.Columns.Count - 1)
                                        {
                                            stringBuilder.Append(string.Concat(dataTable.Rows[j][k].ToString(), "\"\n"));
                                        }
                                    }
                                }
                            }
                            this.WriteitDown(stringBuilder.ToString(), str);
                        }
                        num++;
                        logger.Info(string.Concat(new object[] { num, " out of ", length, " records processed" }));
                    }
                    logger.Info("Program Complete");
                    Console.Read();
                }
                catch (Exception exception)
                {
                    logger.Info(exception);
                }
            }
            private DataTable Execute_Query(string connection, string query)
            {
                DataTable dataTable;
                DataTable dataTable1 = new DataTable();
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connection))
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                        {
                            sqlConnection.Open();
                            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                            {
                                sqlDataAdapter.SelectCommand.CommandTimeout = 1800;
                                sqlDataAdapter.Fill(dataTable1);
                                Console.WriteLine("Query executed successfully");
                            }
                            sqlConnection.Close();
                        }
                    }
                    dataTable = dataTable1;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    dataTable = null;
                }
                return dataTable;
            }
            private string SetHeader(DataTable dt, string seperator, string doubleqout)
            {
                StringBuilder stringBuilder = new StringBuilder();
                string empty = string.Empty;
                try
                {
                    stringBuilder.Append(doubleqout);
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        stringBuilder.Append(string.Concat(dt.Columns[i].ColumnName, seperator));
                    }
                    empty = stringBuilder.ToString().Remove(stringBuilder.Length - 2);
                    empty = string.Concat(empty, "\n");
                }
                catch (Exception exception)
                {
                    logger.Info(exception);
                }
                return empty;
            }
            private void WriteitDown(string data, string basedir)
            {
                logger.Info(data);
                try
                {
                    using (StreamWriter streamWriter = File.AppendText(string.Concat(basedir, "Output_Result.csv")))
                    {
                        streamWriter.Write(data);
                    }
                }
                catch (Exception exception)
                {
                    logger.Info(exception);
                }
            }
        }

        public class SQLWriter
        {
            public void run(string basedir, string filename, DataTable dataTable)
            {
                try
                {
                    Wrtier(basedir, filename, dataTable);
                }
                catch (Exception ex)
                {
                    logger.Info(ex);
                }
            }
            private void Wrtier(string basedir, string filename, DataTable dataTable)
            {
                StringBuilder sb = new StringBuilder();
                bool headerset = false;
                string result = string.Empty;

                string seperator = "\",\"";
                string doubleqout = "\"";

                try
                {
                    if (dataTable != null)
                    {
                        if (dataTable.Rows.Count > 0)
                        {
                            if (!headerset)
                            {
                                logger.Info("Parsing Header");
                                WriteitDown(SetHeader(dataTable, seperator, doubleqout), basedir, filename);
                                headerset = true;
                            }
                            logger.Info("Parsing result");
                            for (int j = 0; j < dataTable.Rows.Count; j++)
                            {
                                sb.Append(doubleqout);
                                for (int k = 0; k < dataTable.Columns.Count; k++)
                                {
                                    if (k != dataTable.Columns.Count - 1)
                                    {
                                        sb.Append(string.Concat(dataTable.Rows[j][k].ToString(), seperator));
                                    }
                                    else if (k == dataTable.Columns.Count - 1)
                                    {
                                        sb.Append(string.Concat(dataTable.Rows[j][k].ToString(), "\"\n"));
                                    }
                                }
                            }
                        }
                        WriteitDown(sb.ToString(), basedir, filename);
                    }
                }
                catch (Exception ex)
                {
                    logger.Info(ex);
                }
            }
            private string SetHeader(DataTable dt, string seperator, string doubleqout)
            {
                StringBuilder stringBuilder = new StringBuilder();
                string empty = string.Empty;
                try
                {
                    stringBuilder.Append(doubleqout);
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        stringBuilder.Append(string.Concat(dt.Columns[i].ColumnName, seperator));
                    }
                    empty = stringBuilder.ToString().Remove(stringBuilder.Length - 2);
                    empty = string.Concat(empty, "\n");
                }
                catch (Exception exception)
                {
                    logger.Info(exception);
                }
                return empty;
            }
            private void WriteitDown(string data, string basedir, string filename)
            {
                logger.Info(data);
                try
                {
                    using (StreamWriter streamWriter = File.AppendText(string.Concat(basedir, filename)))
                    {
                        streamWriter.Write(data);
                    }
                }
                catch (Exception exception)
                {
                    logger.Info(exception);
                }
            }
        }

        public class Splitter
        {
            public Splitter()
            {
            }
            public void IndicationSplitter()
            {
                string str = "C:\\tmp\\INDICATIO_CHUNKS\\Indication_Edit_1081-01-039-074_20181007122110.sql";
                string str1 = "C:\\tmp\\INDICATIO_CHUNKS";
                int num = 400000;
                try
                {
                    using (StreamReader streamReader = new StreamReader(str))
                    {
                        int num1 = 0;
                        while (!streamReader.EndOfStream)
                        {
                            int num2 = 0;
                            using (StreamWriter streamWriter = new StreamWriter(string.Concat(new object[] { str1, "\\Part_", num1, ".sql" })))
                            {
                                streamWriter.AutoFlush = true;
                                Console.WriteLine(string.Concat(new object[] { "Working on File: ", str1, "\\Part_", num1, ".sql" }));
                                using (StreamWriter streamWriter1 = new StreamWriter(string.Concat(new object[] { str1, "\\Part_", num1, ".bat" })))
                                {
                                    streamWriter1.AutoFlush = true;
                                    streamWriter1.WriteLine(string.Concat(new object[] { "SQLCMD -S 10.162.176.85 -d PBMM -U fsheikh -P Dell@100  -i \"C:\\tmp\\Part_", num1, ".sql\" -o \"C:\\tmp\\Part_", num1, "_output.csv\" -W -s,-h-1" }));
                                }
                                while (!streamReader.EndOfStream)
                                {
                                    int num3 = num2 + 1;
                                    num2 = num3;
                                    if (num3 >= num + 1)
                                    {
                                        break;
                                    }
                                    streamWriter.WriteLine(streamReader.ReadLine());
                                }
                                num1++;
                            }
                        }
                        Console.WriteLine("Process Completed !");
                        Console.WriteLine(string.Concat("Files Created: ", num1));
                        Console.WriteLine("Press any key to continue...");
                        Console.Read();
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            public void SplitFileByteWise()
            {
                int num = 0;
                byte[] numArray = new byte[26214400];
                string str = "C:\\tmp\\INDICATIO_CHUNKS\\Indication_Edit_1177-03-102-018_20180807113557.sql";
                string str1 = "C:\\tmp\\INDICATIO_CHUNKS";
                int num1 = 25000000;
                try
                {
                    using (Stream stream = File.OpenRead(str))
                    {
                        int num2 = 0;
                        while (stream.Position < stream.Length)
                        {
                            using (Stream stream1 = File.Create(string.Concat(new object[] { str1, "\\Part_", num2, ".sql" })))
                            {
                                for (int i = num1; i > 0; i -= num)
                                {
                                    int num3 = stream.Read(numArray, 0, Math.Min(i, 26214400));
                                    num = num3;
                                    if (num3 <= 0)
                                    {
                                        break;
                                    }
                                    stream1.Write(numArray, 0, num);
                                }
                            }
                            num2++;
                            Thread.Sleep(500);
                        }
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            public void SplitFileforIndication()
            {
                string str = "C:\\Users\\fansari.INTERNAL\\Desktop\\Splitter\\CustomQuery_Result.csv";
                string str1 = "C:\\Users\\fansari.INTERNAL\\Desktop\\Splitter\\";
                int num = 500000;
                try
                {
                    using (StreamReader streamReader = new StreamReader(str))
                    {
                        int num1 = 0;
                        while (!streamReader.EndOfStream)
                        {
                            int num2 = 0;
                            using (StreamWriter streamWriter = new StreamWriter(string.Concat(new object[] { str1, "\\Part_", num1, ".sql" })))
                            {
                                streamWriter.AutoFlush = true;
                                Console.WriteLine(string.Concat(new object[] { "Working on File: ", str1, "\\Part_", num1, ".sql" }));
                                using (StreamWriter streamWriter1 = new StreamWriter(string.Concat(new object[] { str1, "\\Part_", num1, ".bat" })))
                                {
                                    streamWriter1.AutoFlush = true;
                                    streamWriter1.WriteLine(string.Concat(new object[] { "SQLCMD -S 10.162.176.85 -d PBMM -U fsheikh -P Dell@100  -i \"C:\\tmp\\Part_", num1, ".sql\" -o \"C:\\tmp\\Part_", num1, "_output.csv\" -W -s,-h-1" }));
                                }
                                streamWriter.WriteLine("Changed database context to 'PBMM'.");
                                streamWriter.WriteLine("id,request_id,Provider_license,Provider_Name,Member_No,Drugs_Count,Request_Date,Authorization_Date,Time_in_Seconds,Time_in_Mintues,");
                                streamWriter.WriteLine("--,----------,----------------,-------------,---------,-----------,------------,------------------,---------------,---------------,-");
                                while (!streamReader.EndOfStream)
                                {
                                    int num3 = num2 + 1;
                                    num2 = num3;
                                    if (num3 >= num + 1)
                                    {
                                        break;
                                    }
                                    streamWriter.WriteLine(streamReader.ReadLine());
                                }
                                num1++;
                            }
                        }
                        Console.WriteLine("Process Completed !");
                        Console.WriteLine(string.Concat("Files Created: ", num1));
                        Console.WriteLine("Press any key to continue...");
                        Console.Read();
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            public void SplitFileforJS()
            {
                string str = "C:\\Users\\fansari.INTERNAL\\Desktop\\Splitter\\CustomQuery_Result.csv";
                string str1 = "C:\\Users\\fansari.INTERNAL\\Desktop\\Splitter\\";
                int num = 500000;
                try
                {
                    using (StreamReader streamReader = new StreamReader(str))
                    {
                        int num1 = 0;
                        while (!streamReader.EndOfStream)
                        {
                            int num2 = 0;
                            using (StreamWriter streamWriter = new StreamWriter(string.Concat(new object[] { str1, "\\Part_", num1, ".js" })))
                            {
                                streamWriter.AutoFlush = true;
                                Console.WriteLine(string.Concat(new object[] { "Working on File: ", str1, "\\Part_", num1, ".js" }));
                                while (!streamReader.EndOfStream)
                                {
                                    int num3 = num2 + 1;
                                    num2 = num3;
                                    if (num3 >= num + 1)
                                    {
                                        break;
                                    }
                                    streamWriter.WriteLine(streamReader.ReadLine());
                                }
                                num1++;
                            }
                        }
                        Console.WriteLine("Process Completed !");
                        Console.WriteLine(string.Concat("Files Created: ", num1));
                        Console.WriteLine("Press any key to continue...");
                        Console.Read();
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        public class XMLReader123
        {
            public XMLReader123()
            {
            }
            public string DecryptbyDHCC(string data)
            {
                string message;
                try
                {
                    data = (new EncryptionDecryptionHelper()).DecryptMain(data, "hah");
                    message = data;
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    Console.WriteLine(string.Concat("EXCEPTION OCCURED! Password:", data, "\n", exception.Message));
                    message = exception.Message;
                }
                return message;
            }
            public void DeformXMLtoCSV()
            {
                int i;
                XmlDocument xmlDocument = new XmlDocument();
                StringBuilder stringBuilder = new StringBuilder();
                try
                {
                    string[] strArrays = File.ReadAllLines("C:\\Users\\fansari.INTERNAL\\Desktop\\Rana2.xml");
                    int num = 1;
                    Dictionary<string, string> strs = new Dictionary<string, string>();
                    stringBuilder.Append("PassportNumber|PassportIssuingCountry|License|FullName|username|password|Email|PhoneNumber|Qualification|FacilityLicense|FacilityName|facilities|Location|ActiveFrom|ActiveTo|IsActive|Source|SpecialtyID1|Gender|Nationality|SpecialtyID2|HCType|DHCCSpecialty1|DHCCSpecialty2\n");
                    string[] strArrays1 = stringBuilder.ToString().Split(new char[] { '|' });
                    for (i = 0; i < (int)strArrays1.Length; i++)
                    {
                        strs.Add(strArrays1[i], string.Empty);
                    }
                    strArrays1 = strArrays;
                    for (i = 0; i < (int)strArrays1.Length; i++)
                    {
                        xmlDocument.LoadXml(strArrays1[i]);
                        XmlNodeList childNodes = xmlDocument.SelectSingleNode("HCP").ChildNodes;
                        for (int j = 0; j < childNodes.Count; j++)
                        {
                            if (strs.ContainsKey(childNodes[j].LocalName))
                            {
                                string lower = childNodes[j].LocalName.ToLower();
                                if (lower == "facilities")
                                {
                                    childNodes[j].InnerText = string.Empty;
                                }
                                else if (lower == "password")
                                {
                                    string str = this.DecryptbyDHCC(childNodes[j].InnerText);
                                    string str1 = this.EncryptByLocal(str);
                                    Console.WriteLine(string.Concat(new object[] { "File:", num, " Decrypted:", str, " Encrypted:", str1 }));
                                    childNodes[j].InnerText = str1;
                                }
                                else if (lower == "activefrom")
                                {
                                    childNodes[j].InnerText = Program.ConvertDate(childNodes[j].InnerText);
                                }
                                else if (lower == "activeto")
                                {
                                    childNodes[j].InnerText = Program.ConvertDate(childNodes[j].InnerText);
                                }
                                strs[childNodes[j].LocalName] = childNodes[j].InnerText;
                            }
                        }
                        foreach (string key in strs.Keys)
                        {
                            stringBuilder.Append(string.Concat(strs[key], "|"));
                        }
                        stringBuilder.Append("\n");
                        num++;
                    }
                    using (StreamWriter streamWriter = File.CreateText("C:\\Users\\fansari.INTERNAL\\Desktop\\dhcc\\RANAXML\\Rana_Output.csv"))
                    {
                        streamWriter.Write(stringBuilder.ToString());
                    }
                    Console.WriteLine("File transformation Completed ");
                    Console.Read();
                }
                catch (Exception exception)
                {
                    Console.Write(exception.Message);
                    Console.Read();
                }
            }
            public string EncryptByLocal(string data)
            {
                string message;
                try
                {
                    string str = "dhcc_client";
                    byte[] bytes = Encoding.Unicode.GetBytes(data);
                    using (Aes ae = Aes.Create())
                    {
                        Rfc2898DeriveBytes rfc2898DeriveByte = new Rfc2898DeriveBytes(str, new byte[] { 73, 118, 97, 110, 32, 77, 101, 100, 118, 101, 100, 101, 118 });
                        ae.Key = rfc2898DeriveByte.GetBytes(32);
                        ae.IV = rfc2898DeriveByte.GetBytes(16);
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, ae.CreateEncryptor(), CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(bytes, 0, (int)bytes.Length);
                                cryptoStream.Close();
                            }
                            data = Convert.ToBase64String(memoryStream.ToArray());
                        }
                    }
                    message = data;
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    Console.WriteLine(string.Concat("EXCEPTION OCCURED! Password:", data, "\n", exception.Message));
                    message = exception.Message;
                }
                return message;
            }
            public string GetPassword(string data)
            {
                string message;
                try
                {
                    data = this.EncryptByLocal(this.DecryptbyDHCC(data));
                    message = data;
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    Console.WriteLine(string.Concat("EXCEPTION OCCURED! Password:", data, "\n", exception.Message));
                    message = exception.Message;
                }
                return message;
            }
        }
    }
}