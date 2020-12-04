using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TranslatorUI.DBModels;
using TranslatorUI.Models;

namespace TranslatorUI.Service
{
    public class UserService           //该类封装了User进行的操作
    {
        public string BaseUrl = "http://39.108.211.7/";
        public User User { get; set; }

        public UserService()
        {
            User = new User();
        }

        public bool SignIn(string userName, string password)  //登录
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            byte[] a = System.Text.Encoding.Default.GetBytes(password);
            string passWord = Convert.ToBase64String(a);
            string url = BaseUrl + "user/login?userid=" + userName + "&password=" + passWord;
            var task = client.GetAsync(url);
            bool success = task.Result.IsSuccessStatusCode;
            if (!success)
            {
                return false;
            }
            else
            {
                string i = task.Result.Content.ReadAsStringAsync().Result;
                DBUser dbuser = JsonConvert.DeserializeObject<DBUser>(i);
                this.User.UserId = dbuser.UserID;
                this.User.Coin = dbuser.Wealth;
                return true;
            }
        }

        public bool Ask(string content, int reward)  //提问
        {
            //传过去生成问题，同时传回questionid，生成question,返回question
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string url = BaseUrl + "question/newQuestion";
            DBQuestion dbQuestion = new DBQuestion() { UserID = this.User.UserId, Content = content, Reward = reward };
            HttpContent question = new StringContent(JsonConvert.SerializeObject(dbQuestion), Encoding.UTF8, "application/json");
            var task = client.PostAsync(url, question);
            bool success = task.Result.IsSuccessStatusCode;
            return success;
        }

        public bool Answer(string content, int questionid)    //回答
        {
            //传过去生成回答，同时传回answerid，生成answer，返回answer
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            DBAnswer dbAnswer = new DBAnswer() { UserID = this.User.UserId, Content = content, QuestionID = questionid };
            HttpContent answer = new StringContent(JsonConvert.SerializeObject(dbAnswer), Encoding.UTF8, "application/json");
            string url = BaseUrl + "question/newAnswer";
            var task = client.PostAsync(url, answer);
            bool success = task.Result.IsSuccessStatusCode;
            return success;
        }

        public List<Question> GetMyQuestions(int page)  //我的提问，分页版
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string url = BaseUrl + $"question/questionQuery?userid=" + this.User.UserId + "&page=" + page;
            var task = client.GetAsync(url);
            if (task.Result.IsSuccessStatusCode == false)
            {
                return null;
            }
            else
            {
                string i = task.Result.Content.ReadAsStringAsync().Result;
                List<DBQuestion> dbq = JsonConvert.DeserializeObject<List<DBQuestion>>(i);
                List<Question> qlist = new List<Question>();
                qlist = DBQuestion.ConvertToQList(dbq);
                return qlist;
            }
        }

        public List<Question> GetMyAnswers(int page)   //我的回答，分页版
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string url = BaseUrl + $"question/questionQuery?answerdbyuserid=" + this.User.UserId + "&page=" + page;
            var task = client.GetAsync(url);
            if (task.Result.IsSuccessStatusCode == false)
            {
                return null;
            }
            else
            {
                string i = task.Result.Content.ReadAsStringAsync().Result;
                List<DBQuestion> dbq = JsonConvert.DeserializeObject<List<DBQuestion>>(i);
                List<Question> qlist = new List<Question>();
                qlist = DBQuestion.ConvertToQList(dbq);
                return qlist;
            }
        }
        public bool Adopt(int answerid, int questionid)
        {//采纳回答 
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string url = BaseUrl + "question/adopt?userid=" + this.User.UserId + "&answerid=" + answerid + "&questionid=" + questionid;
            var task = client.PutAsync(url, null);
            bool success = task.Result.IsSuccessStatusCode;
            return success;
        }
        public bool Like(int answerid)    //点赞
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string url = BaseUrl + "question/like?userid=" + this.User.UserId + "&answerid=" + answerid;
            var task = client.PutAsync(url, null);
            bool success = task.Result.IsSuccessStatusCode;
            return success;
        }

    }
}
