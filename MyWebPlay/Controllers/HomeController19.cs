﻿using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Extension;
using MyWebPlay.Model;
using System.Collections;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult XuLySQL13()
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                khoawebsiteClient(null);
                var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                {
                    if (this.HttpContext.Request.Method == "GET")
                    {
                        if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                        {
                            HttpContext.Session.SetObject("google-trick-web", 1);
                            return Redirect("https://google.com");
                        }
                        else
                        {
                            var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                            if (lan != 10)
                            {
                                HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                return Redirect("https://google.com");
                            }
                        }
                    }
                }
                if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
                if (TempData["errorXY"] == "true") return Redirect("https://google.com");
                if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
                if (HttpContext.Session.GetString("TuyetDoi") != null)
                {
                    TempData["UyTin"] = "true";
                    var td = HttpContext.Session.GetString("TuyetDoi");
                    if (td == "true")
                    {
                        TempData["TestTuyetDoi"] = "true"; /*return View();*/
                    }
                    else
                    {
                        TempData["TestTuyetDoi"] = "false";
                    }
                }
                if (TempData["tathoatdong"] == "true")
                {
                    return RedirectToAction("Error");
                }
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
                {
                    HttpContext.Session.Remove("userIP");
                    HttpContext.Session.SetString("userIP", "0.0.0.0");
                    TempData["skipIP"] = "true";
                }
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                var listIP = new List<string>();

                if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")) == false)
                    listIP.Add(HttpContext.Session.GetString("userIP"));
                else
                {
                    TempData["GetDataIP"] = "true";
                    return RedirectToAction("Index");
                }
                khoawebsiteClient(listIP);

                ViewBag.Table = "w2_User";
                ViewBag.Constants = "public const string FIELD_USER_USER_ID = \"user_id\";                                         // ユーザID\r\n\t\tpublic const string FIELD_USER_USER_KBN = \"user_kbn\";                                       // 顧客区分\r\n\t\tpublic const string FIELD_USER_MALL_ID = \"mall_id\";                                         // モールID\r\n\t\tpublic const string FIELD_USER_NAME = \"name\";                                               // 氏名\r\n\t\tpublic const string FIELD_USER_NAME1 = \"name1\";                                             // 氏名1\r\n\t\tpublic const string FIELD_USER_NAME2 = \"name2\";                                             // 氏名2\r\n\t\tpublic const string FIELD_USER_NAME_KANA = \"name_kana\";                                     // 氏名かな\r\n\t\tpublic const string FIELD_USER_NAME_KANA1 = \"name_kana1\";                                   // 氏名かな1\r\n\t\tpublic const string FIELD_USER_NAME_KANA2 = \"name_kana2\";                                   // 氏名かな2\r\n\t\tpublic const string FIELD_USER_NICK_NAME = \"nick_name\";                                     // ニックネーム\r\n\t\tpublic const string FIELD_USER_MAIL_ADDR = \"mail_addr\";                                     // メールアドレス\r\n\t\tpublic const string FIELD_USER_MAIL_ADDR2 = \"mail_addr2\";                                   // メールアドレス2\r\n\t\tpublic const string FIELD_USER_ZIP = \"zip\";                                                 // 郵便番号\r\n\t\tpublic const string FIELD_USER_ZIP1 = \"zip1\";                                               // 郵便番号1\r\n\t\tpublic const string FIELD_USER_ZIP2 = \"zip2\";                                               // 郵便番号2\r\n\t\tpublic const string FIELD_USER_ADDR = \"addr\";                                               // 住所\r\n\t\tpublic const string FIELD_USER_ADDR1 = \"addr1\";                                             // 住所1\r\n\t\tpublic const string FIELD_USER_ADDR2 = \"addr2\";                                             // 住所2\r\n\t\tpublic const string FIELD_USER_ADDR3 = \"addr3\";                                             // 住所3\r\n\t\tpublic const string FIELD_USER_ADDR4 = \"addr4\";                                             // 住所4\r\n\t\tpublic const string FIELD_USER_TEL1 = \"tel1\";                                               // 電話番号1\r\n\t\tpublic const string FIELD_USER_TEL1_1 = \"tel1_1\";                                           // 電話番号1-1\r\n\t\tpublic const string FIELD_USER_TEL1_2 = \"tel1_2\";                                           // 電話番号1-2\r\n\t\tpublic const string FIELD_USER_TEL1_3 = \"tel1_3\";                                           // 電話番号1-3\r\n\t\tpublic const string FIELD_USER_TEL2 = \"tel2\";                                               // 電話番号2\r\n\t\tpublic const string FIELD_USER_TEL2_1 = \"tel2_1\";                                           // 電話番号2-1\r\n\t\tpublic const string FIELD_USER_TEL2_2 = \"tel2_2\";                                           // 電話番号2-2\r\n\t\tpublic const string FIELD_USER_TEL2_3 = \"tel2_3\";                                           // 電話番号2-3\r\n\t\tpublic const string FIELD_USER_TEL3 = \"tel3\";                                               // 電話番号3\r\n\t\tpublic const string FIELD_USER_TEL3_1 = \"tel3_1\";                                           // 電話番号3-1\r\n\t\tpublic const string FIELD_USER_TEL3_2 = \"tel3_2\";                                           // 電話番号3-2\r\n\t\tpublic const string FIELD_USER_TEL3_3 = \"tel3_3\";                                           // 電話番号3-3\r\n\t\tpublic const string FIELD_USER_FAX = \"fax\";                                                 // ＦＡＸ\r\n\t\tpublic const string FIELD_USER_FAX_1 = \"fax_1\";                                             // ＦＡＸ1\r\n\t\tpublic const string FIELD_USER_FAX_2 = \"fax_2\";                                             // ＦＡＸ2\r\n\t\tpublic const string FIELD_USER_FAX_3 = \"fax_3\";                                             // ＦＡＸ3\r\n\t\tpublic const string FIELD_USER_SEX = \"sex\";                                                 // 性別\r\n\t\tpublic const string FIELD_USER_BIRTH = \"birth\";                                             // 生年月日\r\n\t\tpublic const string FIELD_USER_BIRTH_YEAR = \"birth_year\";                                   // 生年月日（年）\r\n\t\tpublic const string FIELD_USER_BIRTH_MONTH = \"birth_month\";                                 // 生年月日（月）\r\n\t\tpublic const string FIELD_USER_BIRTH_DAY = \"birth_day\";                                     // 生年月日（日）\r\n\t\tpublic const string FIELD_USER_COMPANY_NAME = \"company_name\";                               // 企業名\r\n\t\tpublic const string FIELD_USER_COMPANY_POST_NAME = \"company_post_name\";                     // 部署名\r\n\t\tpublic const string FIELD_USER_COMPANY_EXECTIVE_NAME = \"company_exective_name\";             // 役職名\r\n\t\tpublic const string FIELD_USER_ADVCODE_FIRST = \"advcode_first\";                             // 初回広告コード\r\n\t\tpublic const string FIELD_USER_ATTRIBUTE1 = \"attribute1\";                                   // 属性1\r\n\t\tpublic const string FIELD_USER_ATTRIBUTE2 = \"attribute2\";                                   // 属性2\r\n\t\tpublic const string FIELD_USER_ATTRIBUTE3 = \"attribute3\";                                   // 属性3\r\n\t\tpublic const string FIELD_USER_ATTRIBUTE4 = \"attribute4\";                                   // 属性4\r\n\t\tpublic const string FIELD_USER_ATTRIBUTE5 = \"attribute5\";                                   // 属性5\r\n\t\tpublic const string FIELD_USER_ATTRIBUTE6 = \"attribute6\";                                   // 属性6\r\n\t\tpublic const string FIELD_USER_ATTRIBUTE7 = \"attribute7\";                                   // 属性7\r\n\t\tpublic const string FIELD_USER_ATTRIBUTE8 = \"attribute8\";                                   // 属性8\r\n\t\tpublic const string FIELD_USER_ATTRIBUTE9 = \"attribute9\";                                   // 属性9\r\n\t\tpublic const string FIELD_USER_ATTRIBUTE10 = \"attribute10\";                                 // 属性10\r\n\t\tpublic const string FIELD_USER_LOGIN_ID = \"login_id\";                                       // ログインＩＤ\r\n\t\tpublic const string FIELD_USER_PASSWORD = \"password\";                                       // パスワード\r\n\t\tpublic const string FIELD_USER_QUESTION = \"question\";                                       // 質問\r\n\t\tpublic const string FIELD_USER_ANSWER = \"answer\";                                           // 回答\r\n\t\tpublic const string FIELD_USER_CAREER_ID = \"career_id\";\t\t\t\t\t\t\t\t\t\t// キャリアID\r\n\t\tpublic const string FIELD_USER_MOBILE_UID = \"mobile_uid\";                                   // モバイルユーザID\r\n\t\tpublic const string FIELD_USER_REMOTE_ADDR = \"remote_addr\";\t\t\t\t\t\t\t\t\t// リモートIPアドレス\r\n\t\tpublic const string FIELD_USER_MAIL_FLG = \"mail_flg\";                                       // メール配信フラグ\r\n\t\tpublic const string FIELD_USER_USER_MEMO = \"user_memo\";                                     // ユーザメモ\r\n\t\tpublic const string FIELD_USER_DEL_FLG = \"del_flg\";                                         // 削除フラグ\r\n\t\tpublic const string FIELD_USER_DATE_CREATED = \"date_created\";                               // 作成日\r\n\t\tpublic const string FIELD_USER_DATE_CHANGED = \"date_changed\";                               // 更新日\r\n\t\tpublic const string FIELD_USER_LAST_CHANGED = \"last_changed\";                               // 最終更新者\r\n\t\tpublic const string FIELD_USER_MEMBER_RANK_ID = \"member_rank_id\";                           // 会員ランクID\r\n\t\tpublic const string FIELD_USER_RECOMMEND_UID = \"recommend_uid\";\t\t\t\t\t\t\t\t// 外部レコメンド連携用ユーザID\r\n\t\tpublic const string FIELD_USER_DATE_LAST_LOGGEDIN = \"date_last_loggedin\";                   // 最終ログイン日時\r\n\t\tpublic const string FIELD_USER_USER_MANAGEMENT_LEVEL_ID = \"user_management_level_id\";       // ユーザー管理レベルID\r\n\t\tpublic const string FIELD_USER_INTEGRATED_FLG = \"integrated_flg\";                           // ユーザー統合フラグ\r\n\t\tpublic const string FIELD_USER_FIXED_PURCHASE_MEMBER_FLG = \"fixed_purchase_member_flg\";\t\t// 定期会員フラグ\r\n\t\tpublic const string FIELD_USER_EASY_REGISTER_FLG = \"easy_register_flg\";\t\t\t\t\t\t// かんたん会員フラグ\r\n\t\tpublic const string FIELD_USER_ACCESS_COUNTRY_ISO_CODE = \"access_country_iso_code\";         // アクセス国ISOコード\r\n\t\tpublic const string FIELD_USER_DISP_LANGUAGE_CODE = \"disp_language_code\";               // 表示言語コード\r\n\t\tpublic const string FIELD_USER_DISP_LANGUAGE_LOCALE_ID = \"disp_language_locale_id\";     // 表示言語ロケールID\r\n\t\tpublic const string FIELD_USER_DISP_CURRENCY_CODE = \"disp_currency_code\";               // 表示通貨コード\r\n\t\tpublic const string FIELD_USER_DISP_CURRENCY_LOCALE_ID = \"disp_currency_locale_id\";     // 表示通貨ロケールID\r\n\t\tpublic const string FIELD_USER_LAST_BIRTHDAY_POINT_ADD_YEAR = \"last_birthday_point_add_year\";// 最終誕生日ポイント付与年\r\n\t\tpublic const string FIELD_USER_ADDR_COUNTRY_ISO_CODE = \"addr_country_iso_code\";             // 住所国ISOコード\r\n\t\tpublic const string FIELD_USER_ADDR_COUNTRY_NAME = \"addr_country_name\";                     // 住所国名\r\n\t\tpublic const string FIELD_USER_ADDR5 = \"addr5\";                                             // 住所5\r\n\t\tpublic const string FIELD_USER_LAST_BIRTHDAY_COUPON_PUBLISH_YEAR = \"last_birthday_coupon_publish_year\";// 最終誕生日クーポン付与年\r\n\t\t/// <summary>リアルタイム購入回数（注文基準）</summary>\r\n\t\tpublic const string FIELD_USER_ORDER_COUNT_ORDER_REALTIME = \"order_count_order_realtime\";\r\n\t\t/// <summary>過去累計購入回数</summary>\r\n\t\tpublic const string FIELD_USER_ORDER_COUNT_OLD = \"order_count_old\";\r\n\t\t/// <summary>紹介コード</summary>\r\n\t\tpublic const string FIELD_USER_REFERRAL_CODE = \"referral_code\";\r\n\t\t/// <summary>紹介元ユーザーID</summary>\r\n\t\tpublic const string FIELD_USER_REFERRED_USER_ID = \"referred_user_id\";";
                ViewBag.ModelCs = "#region プロパティ\r\n\t\t/// <summary>ユーザID</summary>\r\n\t\t[UpdateData(1, \"user_id\")]\r\n\t\tpublic string UserId\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_USER_ID]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_USER_ID] = value; }\r\n\t\t}\r\n\t\t/// <summary>顧客区分</summary>\r\n\t\t[UpdateData(2, \"user_kbn\")]\r\n\t\tpublic string UserKbn\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_USER_KBN]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_USER_KBN] = value; }\r\n\t\t}\r\n\t\t/// <summary>モールID</summary>\r\n\t\t[UpdateData(3, \"mall_id\")]\r\n\t\tpublic string MallId\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_MALL_ID]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_MALL_ID] = value; }\r\n\t\t}\r\n\t\t/// <summary>氏名</summary>\r\n\t\t[UpdateData(4, \"name\")]\r\n\t\tpublic string Name\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_NAME]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_NAME] = value; }\r\n\t\t}\r\n\t\t/// <summary>氏名1</summary>\r\n\t\t[UpdateData(5, \"name1\")]\r\n\t\tpublic string Name1\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_NAME1]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_NAME1] = value; }\r\n\t\t}\r\n\t\t/// <summary>氏名2</summary>\r\n\t\t[UpdateData(6, \"name2\")]\r\n\t\tpublic string Name2\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_NAME2]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_NAME2] = value; }\r\n\t\t}\r\n\t\t/// <summary>氏名かな</summary>\r\n\t\t[UpdateData(7, \"name_kana\")]\r\n\t\tpublic string NameKana\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_NAME_KANA]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_NAME_KANA] = value; }\r\n\t\t}\r\n\t\t/// <summary>氏名かな1</summary>\r\n\t\t[UpdateData(8, \"name_kana1\")]\r\n\t\tpublic string NameKana1\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_NAME_KANA1]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_NAME_KANA1] = value; }\r\n\t\t}\r\n\t\t/// <summary>氏名かな2</summary>\r\n\t\t[UpdateData(9, \"name_kana2\")]\r\n\t\tpublic string NameKana2\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_NAME_KANA2]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_NAME_KANA2] = value; }\r\n\t\t}\r\n\t\t/// <summary>ニックネーム</summary>\r\n\t\t[UpdateData(10, \"nick_name\")]\r\n\t\tpublic string NickName\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_NICK_NAME]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_NICK_NAME] = value; }\r\n\t\t}\r\n\t\t/// <summary>メールアドレス</summary>\r\n\t\t[UpdateData(11, \"mail_addr\")]\r\n\t\tpublic string MailAddr\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_MAIL_ADDR]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_MAIL_ADDR] = value; }\r\n\t\t}\r\n\t\t/// <summary>メールアドレス2</summary>\r\n\t\t[UpdateData(12, \"mail_addr2\")]\r\n\t\tpublic string MailAddr2\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_MAIL_ADDR2]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_MAIL_ADDR2] = value; }\r\n\t\t}\r\n\t\t/// <summary>郵便番号</summary>\r\n\t\t[UpdateData(13, \"zip\")]\r\n\t\tpublic string Zip\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_ZIP]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_ZIP] = value; }\r\n\t\t}\r\n\t\t/// <summary>郵便番号1</summary>\r\n\t\t[UpdateData(14, \"zip1\")]\r\n\t\tpublic string Zip1\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_ZIP1]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_ZIP1] = value; }\r\n\t\t}\r\n\t\t/// <summary>郵便番号2</summary>\r\n\t\t[UpdateData(15, \"zip2\")]\r\n\t\tpublic string Zip2\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_ZIP2]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_ZIP2] = value; }\r\n\t\t}\r\n\t\t/// <summary>住所</summary>\r\n\t\t[UpdateData(16, \"addr\")]\r\n\t\tpublic string Addr\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_ADDR]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_ADDR] = value; }\r\n\t\t}\r\n\t\t/// <summary>住所1</summary>\r\n\t\t[UpdateData(17, \"addr1\")]\r\n\t\tpublic string Addr1\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_ADDR1]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_ADDR1] = value; }\r\n\t\t}\r\n\t\t/// <summary>住所2</summary>\r\n\t\t[UpdateData(18, \"addr2\")]\r\n\t\tpublic string Addr2\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_ADDR2]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_ADDR2] = value; }\r\n\t\t}\r\n\t\t/// <summary>住所3</summary>\r\n\t\t[UpdateData(19, \"addr3\")]\r\n\t\tpublic string Addr3\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_ADDR3]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_ADDR3] = value; }\r\n\t\t}\r\n\t\t/// <summary>住所4</summary>\r\n\t\t[UpdateData(20, \"addr4\")]\r\n\t\tpublic string Addr4\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_ADDR4]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_ADDR4] = value; }\r\n\t\t}\r\n\t\t/// <summary>電話番号1</summary>\r\n\t\t[UpdateData(21, \"tel1\")]\r\n\t\tpublic string Tel1\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_TEL1]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_TEL1] = value; }\r\n\t\t}\r\n\t\t/// <summary>電話番号1-1</summary>\r\n\t\t[UpdateData(22, \"tel1_1\")]\r\n\t\tpublic string Tel1_1\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_TEL1_1]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_TEL1_1] = value; }\r\n\t\t}\r\n\t\t/// <summary>電話番号1-2</summary>\r\n\t\t[UpdateData(23, \"tel1_2\")]\r\n\t\tpublic string Tel1_2\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_TEL1_2]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_TEL1_2] = value; }\r\n\t\t}\r\n\t\t/// <summary>電話番号1-3</summary>\r\n\t\t[UpdateData(24, \"tel1_3\")]\r\n\t\tpublic string Tel1_3\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_TEL1_3]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_TEL1_3] = value; }\r\n\t\t}\r\n\t\t/// <summary>電話番号2</summary>\r\n\t\t[UpdateData(25, \"tel2\")]\r\n\t\tpublic string Tel2\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_TEL2]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_TEL2] = value; }\r\n\t\t}\r\n\t\t/// <summary>電話番号2-1</summary>\r\n\t\t[UpdateData(26, \"tel2_1\")]\r\n\t\tpublic string Tel2_1\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_TEL2_1]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_TEL2_1] = value; }\r\n\t\t}\r\n\t\t/// <summary>電話番号2-2</summary>\r\n\t\t[UpdateData(27, \"tel2_2\")]\r\n\t\tpublic string Tel2_2\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_TEL2_2]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_TEL2_2] = value; }\r\n\t\t}\r\n\t\t/// <summary>電話番号2-3</summary>\r\n\t\t[UpdateData(28, \"tel2_3\")]\r\n\t\tpublic string Tel2_3\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_TEL2_3]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_TEL2_3] = value; }\r\n\t\t}\r\n\t\t/// <summary>電話番号3</summary>\r\n\t\t[UpdateData(29, \"tel3\")]\r\n\t\tpublic string Tel3\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_TEL3]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_TEL3] = value; }\r\n\t\t}\r\n\t\t/// <summary>電話番号3-1</summary>\r\n\t\t[UpdateData(30, \"tel3_1\")]\r\n\t\tpublic string Tel3_1\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_TEL3_1]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_TEL3_1] = value; }\r\n\t\t}\r\n\t\t/// <summary>電話番号3-2</summary>\r\n\t\t[UpdateData(31, \"tel3_2\")]\r\n\t\tpublic string Tel3_2\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_TEL3_2]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_TEL3_2] = value; }\r\n\t\t}\r\n\t\t/// <summary>電話番号3-3</summary>\r\n\t\t[UpdateData(32, \"tel3_3\")]\r\n\t\tpublic string Tel3_3\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_TEL3_3]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_TEL3_3] = value; }\r\n\t\t}\r\n\t\t/// <summary>ＦＡＸ</summary>\r\n\t\t[UpdateData(33, \"fax\")]\r\n\t\tpublic string Fax\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_FAX]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_FAX] = value; }\r\n\t\t}\r\n\t\t/// <summary>ＦＡＸ1</summary>\r\n\t\t[UpdateData(34, \"fax_1\")]\r\n\t\tpublic string Fax_1\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_FAX_1]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_FAX_1] = value; }\r\n\t\t}\r\n\t\t/// <summary>ＦＡＸ2</summary>\r\n\t\t[UpdateData(35, \"fax_2\")]\r\n\t\tpublic string Fax_2\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_FAX_2]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_FAX_2] = value; }\r\n\t\t}\r\n\t\t/// <summary>ＦＡＸ3</summary>\r\n\t\t[UpdateData(36, \"fax_3\")]\r\n\t\tpublic string Fax_3\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_FAX_3]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_FAX_3] = value; }\r\n\t\t}\r\n\t\t/// <summary>性別</summary>\r\n\t\t[UpdateData(37, \"sex\")]\r\n\t\tpublic string Sex\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_SEX]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_SEX] = value; }\r\n\t\t}\r\n\t\t/// <summary>生年月日</summary>\r\n\t\t[UpdateData(38, \"birth\")]\r\n\t\tpublic DateTime? Birth\r\n\t\t{\r\n\t\t\tget\r\n\t\t\t{\r\n\t\t\t\tif (this.DataSource[Constants.FIELD_USER_BIRTH] == DBNull.Value) return null;\r\n\t\t\t\treturn (DateTime?)this.DataSource[Constants.FIELD_USER_BIRTH];\r\n\t\t\t}\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_BIRTH] = value; }\r\n\t\t}\r\n\t\t/// <summary>生年月日（年）</summary>\r\n\t\t[UpdateData(39, \"birth_year\")]\r\n\t\tpublic string BirthYear\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_BIRTH_YEAR]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_BIRTH_YEAR] = value; }\r\n\t\t}\r\n\t\t/// <summary>生年月日（月）</summary>\r\n\t\t[UpdateData(40, \"birth_month\")]\r\n\t\tpublic string BirthMonth\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_BIRTH_MONTH]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_BIRTH_MONTH] = value; }\r\n\t\t}\r\n\t\t/// <summary>生年月日（日）</summary>\r\n\t\t[UpdateData(41, \"birth_day\")]\r\n\t\tpublic string BirthDay\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_BIRTH_DAY]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_BIRTH_DAY] = value; }\r\n\t\t}\r\n\t\t/// <summary>企業名</summary>\r\n\t\t[UpdateData(42, \"company_name\")]\r\n\t\tpublic string CompanyName\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_COMPANY_NAME]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_COMPANY_NAME] = value; }\r\n\t\t}\r\n\t\t/// <summary>部署名</summary>\r\n\t\t[UpdateData(43, \"company_post_name\")]\r\n\t\tpublic string CompanyPostName\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_COMPANY_POST_NAME]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_COMPANY_POST_NAME] = value; }\r\n\t\t}\r\n\t\t/// <summary>役職名</summary>\r\n\t\t[UpdateData(44, \"company_exective_name\")]\r\n\t\tpublic string CompanyExectiveName\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_COMPANY_EXECTIVE_NAME]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_COMPANY_EXECTIVE_NAME] = value; }\r\n\t\t}\r\n\t\t/// <summary>初回広告コード</summary>\r\n\t\t[UpdateData(45, \"advcode_first\")]\r\n\t\tpublic string AdvcodeFirst\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_ADVCODE_FIRST]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_ADVCODE_FIRST] = value; }\r\n\t\t}\r\n\t\t/// <summary>属性1</summary>\r\n\t\t[UpdateData(46, \"attribute1\")]\r\n\t\tpublic string Attribute1\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_ATTRIBUTE1]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_ATTRIBUTE1] = value; }\r\n\t\t}\r\n\t\t/// <summary>属性2</summary>\r\n\t\t[UpdateData(47, \"attribute2\")]\r\n\t\tpublic string Attribute2\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_ATTRIBUTE2]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_ATTRIBUTE2] = value; }\r\n\t\t}\r\n\t\t/// <summary>属性3</summary>\r\n\t\t[UpdateData(48, \"attribute3\")]\r\n\t\tpublic string Attribute3\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_ATTRIBUTE3]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_ATTRIBUTE3] = value; }\r\n\t\t}\r\n\t\t/// <summary>属性4</summary>\r\n\t\t[UpdateData(49, \"attribute4\")]\r\n\t\tpublic string Attribute4\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_ATTRIBUTE4]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_ATTRIBUTE4] = value; }\r\n\t\t}\r\n\t\t/// <summary>属性5</summary>\r\n\t\t[UpdateData(50, \"attribute5\")]\r\n\t\tpublic string Attribute5\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_ATTRIBUTE5]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_ATTRIBUTE5] = value; }\r\n\t\t}\r\n\t\t/// <summary>属性6</summary>\r\n\t\t[UpdateData(51, \"attribute6\")]\r\n\t\tpublic string Attribute6\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_ATTRIBUTE6]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_ATTRIBUTE6] = value; }\r\n\t\t}\r\n\t\t/// <summary>属性7</summary>\r\n\t\t[UpdateData(52, \"attribute7\")]\r\n\t\tpublic string Attribute7\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_ATTRIBUTE7]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_ATTRIBUTE7] = value; }\r\n\t\t}\r\n\t\t/// <summary>属性8</summary>\r\n\t\t[UpdateData(53, \"attribute8\")]\r\n\t\tpublic string Attribute8\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_ATTRIBUTE8]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_ATTRIBUTE8] = value; }\r\n\t\t}\r\n\t\t/// <summary>属性9</summary>\r\n\t\t[UpdateData(54, \"attribute9\")]\r\n\t\tpublic string Attribute9\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_ATTRIBUTE9]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_ATTRIBUTE9] = value; }\r\n\t\t}\r\n\t\t/// <summary>属性10</summary>\r\n\t\t[UpdateData(55, \"attribute10\")]\r\n\t\tpublic string Attribute10\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_ATTRIBUTE10]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_ATTRIBUTE10] = value; }\r\n\t\t}\r\n\t\t/// <summary>ログインＩＤ</summary>\r\n\t\t[UpdateData(56, \"login_id\")]\r\n\t\tpublic string LoginId\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_LOGIN_ID]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_LOGIN_ID] = value; }\r\n\t\t}\r\n\t\t/// <summary>暗号化されているパスワード</summary>\r\n\t\t[UpdateData(57, \"password\")]\r\n\t\tpublic string Password\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_PASSWORD]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_PASSWORD] = value; }\r\n\t\t}\r\n\t\t/// <summary>質問</summary>\r\n\t\t[UpdateData(58, \"question\")]\r\n\t\tpublic string Question\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_QUESTION]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_QUESTION] = value; }\r\n\t\t}\r\n\t\t/// <summary>回答</summary>\r\n\t\t[UpdateData(59, \"answer\")]\r\n\t\tpublic string Answer\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_ANSWER]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_ANSWER] = value; }\r\n\t\t}\r\n\t\t/// <summary>キャリアID</summary>\r\n\t\t[UpdateData(60, \"career_id\")]\r\n\t\tpublic string CareerId\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_CAREER_ID]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_CAREER_ID] = value; }\r\n\t\t}\r\n\t\t/// <summary>モバイルUID</summary>\r\n\t\t[UpdateData(61, \"mobile_uid\")]\r\n\t\tpublic string MobileUid\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_MOBILE_UID]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_MOBILE_UID] = value; }\r\n\t\t}\r\n\t\t/// <summary>リモートIPアドレス</summary>\r\n\t\t[UpdateData(62, \"remote_addr\")]\r\n\t\tpublic string RemoteAddr\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_REMOTE_ADDR]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_REMOTE_ADDR] = value; }\r\n\t\t}\r\n\t\t/// <summary>メール配信フラグ</summary>\r\n\t\t[UpdateData(63, \"mail_flg\")]\r\n\t\tpublic string MailFlg\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_MAIL_FLG]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_MAIL_FLG] = value; }\r\n\t\t}\r\n\t\t/// <summary>ユーザメモ</summary>\r\n\t\t[UpdateData(64, \"user_memo\")]\r\n\t\tpublic string UserMemo\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_USER_MEMO]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_USER_MEMO] = value; }\r\n\t\t}\r\n\t\t/// <summary>削除フラグ</summary>\r\n\t\t[UpdateData(65, \"del_flg\")]\r\n\t\tpublic string DelFlg\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_DEL_FLG]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_DEL_FLG] = value; }\r\n\t\t}\r\n\t\t/// <summary>作成日</summary>\r\n\t\t[UpdateData(66, \"date_created\")]\r\n\t\tpublic DateTime DateCreated\r\n\t\t{\r\n\t\t\tget { return (DateTime)this.DataSource[Constants.FIELD_USER_DATE_CREATED]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_DATE_CREATED] = value; }\r\n\t\t}\r\n\t\t/// <summary>更新日</summary>\r\n\t\t[UpdateData(67, \"date_changed\")]\r\n\t\tpublic DateTime DateChanged\r\n\t\t{\r\n\t\t\tget { return (DateTime)this.DataSource[Constants.FIELD_USER_DATE_CHANGED]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_DATE_CHANGED] = value; }\r\n\t\t}\r\n\t\t/// <summary>最終更新者</summary>\r\n\t\t[UpdateData(68, \"last_changed\")]\r\n\t\tpublic string LastChanged\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_LAST_CHANGED]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_LAST_CHANGED] = value; }\r\n\t\t}\r\n\t\t/// <summary>会員ランクID</summary>\r\n\t\t[UpdateData(69, \"member_rank_id\")]\r\n\t\tpublic string MemberRankId\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_MEMBER_RANK_ID]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_MEMBER_RANK_ID] = value; }\r\n\t\t}\r\n\t\t/// <summary>外部レコメンド連携用ユーザID</summary>\r\n\t\t[UpdateData(70, \"recommend_uid\")]\r\n\t\tpublic string RecommendUid\r\n\t\t{\r\n\t\t\tget\r\n\t\t\t{\r\n\t\t\t\tif (this.DataSource[Constants.FIELD_USER_RECOMMEND_UID] == DBNull.Value) return null;\r\n\t\t\t\treturn (string)this.DataSource[Constants.FIELD_USER_RECOMMEND_UID];\r\n\t\t\t}\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_RECOMMEND_UID] = value; }\r\n\t\t}\r\n\t\t/// <summary>最終ログイン日時</summary>\r\n\t\t[UpdateData(71, \"date_last_loggedin\")]\r\n\t\tpublic DateTime? DateLastLoggedin\r\n\t\t{\r\n\t\t\tget\r\n\t\t\t{\r\n\t\t\t\tif (this.DataSource[Constants.FIELD_USER_DATE_LAST_LOGGEDIN] == DBNull.Value) return null;\r\n\t\t\t\treturn (DateTime?)this.DataSource[Constants.FIELD_USER_DATE_LAST_LOGGEDIN];\r\n\t\t\t}\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_DATE_LAST_LOGGEDIN] = value; }\r\n\t\t}\r\n\t\t/// <summary>ユーザー管理レベルID</summary>\r\n\t\t[UpdateData(72, \"user_management_level_id\")]\r\n\t\tpublic string UserManagementLevelId\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_USER_MANAGEMENT_LEVEL_ID]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_USER_MANAGEMENT_LEVEL_ID] = value; }\r\n\t\t}\r\n\t\t/// <summary>ユーザー統合フラグ</summary>\r\n\t\t[UpdateData(73, \"integrated_flg\")]\r\n\t\tpublic string IntegratedFlg\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_INTEGRATED_FLG]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_INTEGRATED_FLG] = value; }\r\n\t\t}\r\n\t\t/// <summary>かんたん会員フラグ</summary>\r\n\t\t[UpdateData(74, \"easy_register_flg\")]\r\n\t\tpublic string EasyRegisterFlg\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_EASY_REGISTER_FLG]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_EASY_REGISTER_FLG] = value; }\r\n\t\t}\r\n\t\t/// <summary>定期会員フラグ</summary>\r\n\t\t[UpdateData(75, \"fixed_purchase_member_flg\")]\r\n\t\tpublic string FixedPurchaseMemberFlg\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_FIXED_PURCHASE_MEMBER_FLG]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_FIXED_PURCHASE_MEMBER_FLG] = value; }\r\n\t\t}\r\n\t\t/// <summary>アクセス国ISOコード</summary>\r\n\t\t[UpdateDataAttribute(76, \"access_country_iso_code\")]\r\n\t\tpublic string AccessCountryIsoCode\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_ACCESS_COUNTRY_ISO_CODE]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_ACCESS_COUNTRY_ISO_CODE] = value; }\r\n\t\t}\r\n\t\t/// <summary>表示言語コード</summary>\r\n\t\t[UpdateDataAttribute(77, \"disp_language_code\")]\r\n\t\tpublic string DispLanguageCode\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_DISP_LANGUAGE_CODE]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_DISP_LANGUAGE_CODE] = value; }\r\n\t\t}\r\n\t\t/// <summary>表示言語ロケールID</summary>\r\n\t\t[UpdateDataAttribute(78, \"disp_language_locale_id\")]\r\n\t\tpublic string DispLanguageLocaleId\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_DISP_LANGUAGE_LOCALE_ID]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_DISP_LANGUAGE_LOCALE_ID] = value; }\r\n\t\t}\r\n\t\t/// <summary>表示通貨コード</summary>\r\n\t\t[UpdateDataAttribute(79, \"disp_currency_code\")]\r\n\t\tpublic string DispCurrencyCode\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_DISP_CURRENCY_CODE]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_DISP_CURRENCY_CODE] = value; }\r\n\t\t}\r\n\t\t/// <summary>表示通貨ロケールID</summary>\r\n\t\t[UpdateDataAttribute(80, \"disp_currency_locale_id\")]\r\n\t\tpublic string DispCurrencyLocaleId\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_DISP_CURRENCY_LOCALE_ID]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_DISP_CURRENCY_LOCALE_ID] = value; }\r\n\t\t}\r\n\t\t/// <summary>最終誕生日ポイント付与年</summary>\r\n\t\t[UpdateDataAttribute(81, \"last_birthday_point_add_year\")]\r\n\t\tpublic string LastBirthdayPointAddYear\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_LAST_BIRTHDAY_POINT_ADD_YEAR]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_LAST_BIRTHDAY_POINT_ADD_YEAR] = value; }\r\n\t\t}\r\n\t\t/// <summary>住所国ISOコード</summary>\r\n\t\t[UpdateDataAttribute(82, \"addr_country_iso_code\")]\r\n\t\tpublic string AddrCountryIsoCode\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_ADDR_COUNTRY_ISO_CODE]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_ADDR_COUNTRY_ISO_CODE] = value; }\r\n\t\t}\r\n\t\t/// <summary>住所国名</summary>\r\n\t\t[UpdateDataAttribute(83, \"addr_country_name\")]\r\n\t\tpublic string AddrCountryName\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_ADDR_COUNTRY_NAME]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_ADDR_COUNTRY_NAME] = value; }\r\n\t\t}\r\n\t\t/// <summary>住所5</summary>\r\n\t\t[UpdateDataAttribute(84, \"addr5\")]\r\n\t\tpublic string Addr5\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_ADDR5]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_ADDR5] = value; }\r\n\t\t}\r\n\t\t/// <summary>最終誕生日クーポン付与年</summary>\r\n\t\t[UpdateDataAttribute(85, \"last_birthday_coupon_publish_year\")]\r\n\t\tpublic string LastBirthdayCouponPublishYear\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_LAST_BIRTHDAY_COUPON_PUBLISH_YEAR]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_LAST_BIRTHDAY_COUPON_PUBLISH_YEAR] = value; }\r\n\t\t}\r\n\t\t/// <summary>リアルタイム購入回数（注文基準）</summary>\r\n\t\t[UpdateData(86, \"order_count_order_realtime\")]\r\n\t\tpublic int OrderCountOrderRealtime\r\n\t\t{\r\n\t\t\tget { return (int)this.DataSource[Constants.FIELD_USER_ORDER_COUNT_ORDER_REALTIME]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_ORDER_COUNT_ORDER_REALTIME] = value; }\r\n\t\t}\r\n\t\t/// <summary>過去累計購入回数</summary>\r\n\t\t[UpdateData(87, \"order_count_old\")]\r\n\t\tpublic int OrderCountOld\r\n\t\t{\r\n\t\t\tget { return (int)this.DataSource[Constants.FIELD_USER_ORDER_COUNT_OLD]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_ORDER_COUNT_OLD] = value; }\r\n\t\t}\r\n\t\t/// <summary>紹介コード</summary>\r\n\t\t[UpdateData(88, \"referral_code\")]\r\n\t\tpublic string ReferralCode\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_REFERRAL_CODE]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_REFERRAL_CODE] = value; }\r\n\t\t}\r\n\t\t/// <summary>紹介元ユーザーID</summary>\r\n\t\t[UpdateData(89, \"referred_user_id\")]\r\n\t\tpublic string ReferredUserId\r\n\t\t{\r\n\t\t\tget { return (string)this.DataSource[Constants.FIELD_USER_REFERRED_USER_ID]; }\r\n\t\t\tset { this.DataSource[Constants.FIELD_USER_REFERRED_USER_ID] = value; }\r\n\t\t}\r\n\t\t#endregion";
                return View();
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl", context:HttpContext);
                }
                return RedirectToAction("Error", new
                {
                    exception = (((int)new Random().Next(0, 100)) % 2 == 0) ? "yes" : "show-error(true)"
                });
            }
        }

        [HttpPost]
        public ActionResult XuLySQL13(IFormCollection f, IFormFile fileData)
        {
            var nix = "";
            var exter = false;
            var linkdown = false;
            var listIP = new List<string>();
            try
            {
                var fix = "";
                foreach (var item in f.Keys)
                {
                    fix += string.Format("{0} : {1}\n", item, f[item]);
                }

                HttpContext.Session.SetString("hanhdong_3275", fix);

                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidung = FileExtension.ReadFile(path);

                var listSettingS = noidung.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoX = listSettingS[48].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (infoX[1] == "true")
                    exter = true;
                var infoY = listSettingS[8].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
                if (infoY[1] == "true") linkdown = true;

               ghilogrequest(f); if (exter == false)
                {
                    TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                    khoawebsiteClient(null);
                    var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                    if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                    {
                        if (this.HttpContext.Request.Method == "GET")
                        {
                            if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                            {
                                HttpContext.Session.SetObject("google-trick-web", 1);
                                return Redirect("https://google.com");
                            }
                            else
                            {
                                var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                                if (lan != 10)
                                {
                                    HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                    return Redirect("https://google.com");
                                }
                            }
                        }
                    }
                    if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
                    if (TempData["errorXY"] == "true") return Redirect("https://google.com");
                    if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
                    if (HttpContext.Session.GetString("TuyetDoi") != null)
                    {
                        TempData["UyTin"] = "true";
                        var td = HttpContext.Session.GetString("TuyetDoi");
                        if (td == "true")
                        {
                            TempData["TestTuyetDoi"] = "true"; /*return View();*/
                        }
                        else
                        {
                            TempData["TestTuyetDoi"] = "false";
                        }
                    }
                    if (TempData["tathoatdong"] == "true")
                    {
                        return RedirectToAction("Error");
                    }
                    if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                    if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                    if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
                    {
                        HttpContext.Session.Remove("userIP");
                        HttpContext.Session.SetString("userIP", "0.0.0.0");
                        TempData["skipIP"] = "true";
                    }
                    /*HttpContext.Session.Remove("ok-data");*/

                    TempData["dataPost"] = "[POST]";
                    HttpContext.Session.SetString("data-result", "true");
                    TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                    //var listIP = new List<string>();

                    if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")) == false)
                        listIP.Add(HttpContext.Session.GetString("userIP"));
                    else
                    {
                        TempData["GetDataIP"] = "true";
                        return RedirectToAction("Index");
                    }
                }
                var table = f["txtTable"].ToString();
                var constants = f["txtConstants"].ToString().Replace("\r\n", "\n").Replace("\n", "\r\n").Trim('\t').Trim(' ').Replace("\r", "").Split("\n");
                var modelCs = f["txtModelCs"].ToString().Replace("\r\n", "\n").Replace("\n", "\r\n");

                if (f.ContainsKey("txtAPI") || (fileData != null && string.IsNullOrEmpty(fileData.FileName) == false))
                {
                    var txtAPI = f["txtAPI"].ToString().Replace("[T-PLAY]", "\t").Replace("[N-PLAY]", "\n").Replace("[R-PLAY]", "\r");
                    if (fileData != null && string.IsNullOrEmpty(fileData.FileName) == false)
                    {
                        if (fileData.FileName.EndsWith(".txt"))
                        {
                            using (var reader = new StreamReader(fileData.OpenReadStream()))
                            {
                                string content = reader.ReadToEnd();
                                txtAPI = content;
                            }
                        }
                    }
                    var apiValue = txtAPI.ToString().Replace("\r", "").Split("\n||\n");
                    table = apiValue[0].Replace("[Empty]", "").Replace("[T-PLAY]", "\t").Replace("[N-PLAY]", "\n").Replace("[R-PLAY]", "\r");
                    constants = apiValue[1].Replace("[Empty]", "").Replace("[T-PLAY]", "\t").Replace("[N-PLAY]", "\n").Replace("[R-PLAY]", "\r").ToString().Replace("\r\n", "\n").Replace("\n", "\r\n").Trim('\t').Trim(' ').Replace("\r", "").Split("\n");
                    modelCs = apiValue[2].Replace("[Empty]", "").Replace("[T-PLAY]", "\t").Replace("[N-PLAY]", "\n").Replace("[R-PLAY]", "\r");
                }


                TempData["dataPost"] = "[" + f["txtConstants"].ToString().Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t") + "]";

               ghilogrequest(f); if (exter == false)
                {
                    khoawebsiteClient(listIP);
                    HttpContext.Session.Remove("ok-data");
                    Calendar xi = CultureInfo.InvariantCulture.Calendar;

                    var delayTime = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "");
                    var partDelayTime = delayTime.Split("#");
                    var hourDL = partDelayTime[0].Replace("H", "");
                    var minDL = partDelayTime[1].Replace("M", "");
                    var secDL = partDelayTime[2].Replace("S", "");

                    var xuxu = xi.AddHours(DateTime.UtcNow, 7);

                    if (hourDL.Contains("-"))
                    {
                       xuxu = xuxu.AddHours(-1 * int.Parse(hourDL.Replace("-", "")));
                    }
                    else
                    {
                        xuxu = xuxu.AddHours(int.Parse(hourDL));
                    }

                    if (minDL.Contains("-"))
                    {
                        xuxu = xuxu.AddMinutes(-1 * int.Parse(minDL.Replace("-", "")));
                    }
                    else
                    {
                        xuxu = xuxu.AddHours(int.Parse(minDL));
                    }

                    if (secDL.Contains("-"))
                    {
                        xuxu = xuxu.AddSeconds(-1 * int.Parse(secDL.Replace("-", "")));
                    }
                    else
                    {
                        xuxu.AddSeconds(int.Parse(secDL));
                    }

                    if (xuxu.Hour >= 6 && xuxu.Hour <= 17)
                    {
                        TempData["mau_background"] = "white";
                        TempData["mau_text"] = "black";
                        TempData["mau_nen"] = "dodgerblue";
                        TempData["winx"] = "❤";
                    }
                    else
                    {
                        TempData["mau_background"] = "black";
                        TempData["mau_text"] = "white";
                        TempData["mau_nen"] = "rebeccapurple";
                        TempData["winx"] = "❤";
                    }
                    var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                    var noidungX = FileExtension.ReadFile(pathX);
                    var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                    var flag = 0;
                    for (int i = 0; i < listSetting.Length; i++)
                    {
                        var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                        if (flag == 0 && (info[0] == "Email_Upload_User" ||
                            info[0] == "MegaIo_Upload_User" || info[0] == "Email_TracNghiem_Create" ||
                            info[0] == "Email_TracNghiem_Update" || info[0] == "Email_Question" ||
                            info[0] == "Email_User_Website" || info[0] == "Email_User_Continue" ||
                            info[0] == "Email_Note" || info[0] == "Email_Karaoke"))
                        {
                            if (info[1] == "false")
                            {

                                TempData["mau_winx"] = "red";
                                flag = 1;
                            }
                            else
                            {

                                TempData["mau_winx"] = "deeppink";
                                flag = 0;
                            }
                        }
                    }
                }

                ViewBag.Table = f["txtTable"].ToString();
                ViewBag.Constants = f["txtConstants"].ToString();
                ViewBag.ModelCs = f["txtModelCs"].ToString();

                var result = "";

                var listFieldConstants = new Hashtable();
                for (int i = 0; i < constants.Length; i++)
                {
                    if (constants[i].Contains("<summary>"))
                        continue;

                    var xy = constants[i].Split(" = \"");
                    var xyy = xy[1].Split("\";");
                    listFieldConstants.Add(xy[0].Replace("public const string ", "").Trim(' ').Trim('\t'), xyy[0]);
                }

                var listFieldModelCs = new Hashtable();

                Regex regExp = new Regex("return .*this.DataSource.*;");
                var ss = "";
                foreach (Match match in regExp.Matches(modelCs))
                {
                    ss += match.Value + "\n";
                }

                ss = ss.Replace("return (", "").Replace("this.DataSource", "").Replace("];", "");

                var model = ss.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < model.Length; i++)
                {
                    var xu = model[i].Split(")[");
                    listFieldModelCs.Add(xu[1].Replace("Constants.", ""), xu[0]);
                }

                foreach (var xin in listFieldConstants.Keys)
                {
                    if (listFieldModelCs.ContainsKey(xin))
                    {
                        var kieu = "";

                        switch (listFieldModelCs[xin])
                        {
                            case "string":
                            case "String":
                                kieu = "nvarchar(max)";
                                break;

                            case "datetime":
                            case "DateTime":
                            case "Datetime":
                                kieu = "datetime";
                                break;

                            case "decimal":
                            case "Decimal":
                            case "Double":
                            case "double":
                                kieu = "decimal(18,3)";
                                break;

                            case "int":
                                kieu = "int";
                                break;

                            case "long":
                                kieu = "long";
                                break;

                            default:
                                kieu = listFieldModelCs[xin].ToString().Replace("?", "");
                                break;

                        }
                        result += "\n\nIF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + table + "' AND COLUMN_NAME = '" + listFieldConstants[xin] + "')\r\nBEGIN\r\n\tALTER TABLE " + table + " ADD " + listFieldConstants[xin] + " " + kieu + "\r\nEND";
                    }
                    else
                    {
                        result += "\n\nIF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + table + "' AND COLUMN_NAME = '" + listFieldConstants[xin] + "')\r\nBEGIN\r\n\tALTER TABLE " + table + " ADD " + listFieldConstants[xin] + " nvarchar(500)" + "\r\nEND";
                    }
                }

                result += "\n\nPRINT N'XONG, QUÁ TRÌNH XỬ LÝ HOÀN TẤT!'";
                nix = result;
                result = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button>&nbsp;&nbsp;&nbsp;<a style=\"color:deeppink\" href=\""+  HttpContext.Request.Path.ToString() + "\">Làm mới</a><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

                Calendar soi = CultureInfo.InvariantCulture.Calendar;
                var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", "");
                if (string.IsNullOrEmpty(chim)) chim = "Default";
                var cuctac = soi.AddHours(DateTime.UtcNow, 7) + "_" + chim;
                var sox = (f.ContainsKey("resultX") == false || f["resultX"] == "false") ? Path.Combine(_webHostEnvironment.WebRootPath,
                    "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt") :
                  Path.Combine(_webHostEnvironment.WebRootPath, "ResultExternal", "data.txt");

                TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt";
                new FileInfo(sox).Create().Dispose();
                FileExtension.WriteFile(sox, nix);
                ViewBag.Result = result;

                ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
                if (TempData["ConnectLinkDown"] == "true") return Redirect("/Home/ViewFile?path=/POST_DataResult/" + TempData["fileResult"]);

               ghilogrequest(f); if (exter == false)
                    return View();
                else
                {
                    if (linkdown == true) return Redirect("/Home/ViewFile?path=/POST_DataResult/" + TempData["fileResult"]);
                    return Ok(new
                    {
                        result = "http://" + Request.Host + "/POST_DataResult/" + TempData["fileResult"].ToString().Replace(" ", "%20")
                    });
                }
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl", context:HttpContext);
                }
               ghilogrequest(f); if (exter == false)
                    return RedirectToAction("Error", new
                    {
                        exception = (((int)new Random().Next(0, 100)) % 2 == 0) ? "yes" : "show-error(true)"
                    });
                return Ok(new
                {
                    error = HttpContext.Session.GetObject<string>("error_exception_log")
                });
            }
        }
    }
}