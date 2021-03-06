using Project.Managers;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project.BackAdmin
{
    public partial class EditWithMain : System.Web.UI.Page
    {
        private bool _isEditMode = true;
        private AccountManager _mgr = new AccountManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.Request.QueryString["UserID"]))
                _isEditMode = true;
            else
                _isEditMode = false;


            if (_isEditMode)
            {
                string idText = this.Request.QueryString["UserID"];
                Guid id;
                if (!Guid.TryParse(idText, out id))
                    this.lblMsg.Text = "id 錯誤";
                else
                    this.InitEditMode(id);
            }
            else
            {
                this.InitCreateMode();
            }
        }
        private void InitCreateMode()
        {
            this.ltlAccount.Visible = false;
            this.txtAccount.Visible = true;

            this.ltlID.Text = "尚待新增";
        }
        private void InitEditMode(Guid id)
        {
            this.ltlAccount.Visible = true;
            this.txtAccount.Visible = false;

            MemberAccount member = this._mgr.GetAccount(id);

            if (member == null)
            {
                this.lblMsg.Text = "查無此 id ";
                return;
            }

            this.ltlAccount.Text = member.Account;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string account = this.txtAccount.Text.Trim();
            string pwd = this.txtPassword.Text.Trim();
            string phone = this.txtPhone.Text.Trim();
            string Email = this.txtEmail.Text.Trim();
            string UserLev = this.txtLevel.Text.Trim();


            if (!_isEditMode)
            {
                MemberAccount member = new MemberAccount();
                member.Account = account;
                member.Password = pwd;
                member.Email = Email;
                member.Phone = phone;
                int i = Convert.ToInt32(UserLev);
                member.UserLevel = i;
                if (this._mgr.GetAccount(account) != null)
                {
                    this.lblMsg.Text = "帳號已存在";
                    return;
                }
                else
                {
                    this._mgr.CreateAccount(member);

                    Response.Redirect("AdminOnlyMaster.aspx");
                }
            }
            else
            {
                string idText = this.Request.QueryString["UserID"];


                Guid id;
                if (!Guid.TryParse(idText, out id))
                {
                    this.lblMsg.Text = "id 錯誤";
                    return;
                }
                MemberAccount member = this._mgr.GetAccount(id);
                member.Password = pwd;
                member.Email = Email;
                member.Phone = phone;
                if (string.IsNullOrWhiteSpace(UserLev))
                {
                    UserLev = this.Request.QueryString["UserLevel"];
                }
                else
                {
                    UserLev = this.txtLevel.Text.Trim();
                }
                int i = Convert.ToInt32(UserLev);
                member.UserLevel = i;
                this._mgr.UpdateAccount(member);
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (this._mgr.IsLogined())
            {
                Response.Redirect("AdminOnlyMaster.aspx");
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (this._mgr.IsLogined())
            {
                Response.Redirect("AdminOnlyMaster.aspx");
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
    }
}