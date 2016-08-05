/// ###################################
/// # OKIT pvt ltd
/// # ---------------------------------
/// # Class to handle session variables;
/// # --------------------------------- 
/// # =================================
/// # Historique:
/// # ---------------------------------
/// # Created By: Vijay chandar on Date: 15-June-2012
/// # Modified By:
/// ###################################


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DistriSoft;
/// <summary>
/// Summary description for SessionHandler
/// </summary>
/// 
namespace DistriSoft
{
    #region EnumSessions
    /// <summary>
    /// Enumarator for Session Variables
    /// </summary>
    public enum enumSessions
    {
        Lang, ClientID,UserID, Profil_Code
    };
    #endregion


    public class SessionHandler
    {
        #region Declaration

        private Page _page; ///Variable for the page class
        #endregion

        #region Properties
        ///Properties for LanguageID and ClientID
        private string _language;
        private string _clientID;
        private string _userID;
        private string _profil_code;
        
        public string Language
        {
            get
            {
                return _language;
            }
        }
        public string ClientID
        {
            get
            {
                return _clientID;
            }
        }
        public string UserID
        {
            get
            {
                return _userID;
            }
        }
        public string profil_code
        {
            get
            {
                return _profil_code;
            }
        }

        #endregion

        #region SessionHandler Method
        /// <summary>
        /// Method for Session Handler
        /// The method gets the page and retrives the session values and sets to the properties LanguageID and ClientID
        /// If there is no session values, then it automatically redirects to the Login page;
        /// </summary>
        /// <param name="p">Page where the session is available</param>
        public SessionHandler(Page p)
        {
            try
            {
                ///Initializes the page variable
                _page = p;

                if (_page.Session[enumSessions.Lang.ToString()] != null) //Checks whether the session variables are avaiable.
                {
                    ///Assigns the session values to the properties
                    _language = _page.Session[enumSessions.Lang.ToString()].ToString();
                    _clientID = _page.Session[enumSessions.ClientID.ToString()].ToString();
                    _userID = _page.Session[enumSessions.UserID.ToString()].ToString();
                    _profil_code = _page.Session[enumSessions.Profil_Code.ToString()].ToString();
                }
                else
                {
                    ///If the session variables are not available then it redirects to the Login page;
                    p.Response.Redirect("~/Login.aspx");
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}