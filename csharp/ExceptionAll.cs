/// ###################################
/// # OKIT pvt ltd
/// # ---------------------------------
/// # Class Exception for Log File;
/// # --------------------------------- 
/// # =================================
/// # Historique:
/// # ---------------------------------
/// # Created By: Ginu on Date: 13-June-2012
/// # Modified By: Vijay chandar on 21-Aug-2012;Added a method Log to create only log of exceptions
/// ###################################
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Net.Mail;

/// <summary>
/// </summary>


public static class EXClass
{

    public static void ExtoLog(Exception ex)
    {
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        logger.Debug(ex.Message);

        string referred = HttpContext.Current.Request.Url.ToString();           //Gets the previous page
        if (ex.InnerException != null)
        {
            HttpContext.Current.Session.Add("Exception", ex.InnerException);     //Checks whether there is any inner exception
            HttpContext.Current.Response.Redirect("~/Error/ErrorPage.aspx?redirect=" + referred);       //Redirects the page to Error page
        }
        else if (ex != null)
        {
            HttpContext.Current.Session.Add("Exception", ex);                   ///Checks whether there is inner exception
            HttpContext.Current.Response.Redirect("~/Error/ErrorPage.aspx?redirect=" + referred);       //Redirects the page to Error page
        }

    }

    #region Exception log
    public static void Log(Exception ex)
    {
        ///Logs the exception
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        logger.Debug(ex.Message);
    }
    #endregion
}