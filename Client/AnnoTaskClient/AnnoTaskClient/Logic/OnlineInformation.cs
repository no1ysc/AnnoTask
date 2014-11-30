using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mshtml;
using SHDocVw;
using System.Web;

namespace AnnoTaskClient.Logic
{
    class OnlineInformation
    {
        InternetExplorer internetExplorer = null;
        IWebBrowserApp webBrowser = null;
        
        public class OnlineInfo
		{
            private string word;
            public ArrayList wordInforSet = new ArrayList();

            public string Word
            {
                get { return word; }
                set { word = value; }
            }
        }

        public class WordInfor
        {
            public string POS;
            public string definitions;
        }

        public OnlineInfo getInfo(string word)
        {
            OnlineInfo workingInfo = new OnlineInfo();

            //1. set word
            workingInfo.Word = word;

            //2. make url for a word
            string encodedWord = HttpUtility.UrlEncode(word);
            string url = "http://krdic.naver.com/search.nhn?query=" + encodedWord + "&kind=keyword";
  
            //3. get pos & definitions
            workingInfo.wordInforSet = getPosDefInfor(url);
            
            //4. return OnlineInfo
            return workingInfo;
        }

        private ArrayList getPosDefInfor(string url)
        {
            HTMLDocument HTML = null;
            ArrayList docURLs = new ArrayList();
            ArrayList wordInforSet = new ArrayList();

            /*
            HTML = openWebBrowser(url);
            getDocURLs(HTML, docURLs);
            closeWebBrowser();

            if (docURLs == null)
            {
                return null;
            }

            for (int idx = 0; idx < docURLs.Count; ++idx)
            {
                WordInfor tempInfor = new WordInfor();
                getPOSDefinition(docURLs[idx].ToString(), tempInfor);

                if (tempInfor.POS != null && tempInfor.definitions != null)
                {
                    wordInforSet.Add(tempInfor);
                }
            }
            */

            HTML = openWebBrowser(url);
            if (HTML == null)
            {
                closeWebBrowser();
                return null;
            }
            getPosDefInfor(HTML, wordInforSet);
            closeWebBrowser();

            return wordInforSet;
        }

        private HTMLDocument openWebBrowser(string url)
        {
            HTMLDocument HTML = null;
            int tryIdx;

            for (tryIdx = 0; tryIdx < 3; tryIdx++)
            {
                try
                {
                    internetExplorer = new InternetExplorer();
                    webBrowser = (IWebBrowserApp)internetExplorer;
                    webBrowser.Visible = false;
                    webBrowser.Navigate(url);
                    while (webBrowser.Busy) ;
                    System.Threading.Thread.Sleep(1000);
                    HTML = (HTMLDocument)webBrowser.Document;
                }
                catch (Exception e)
                {
                    closeWebBrowser();
                    continue;
                }
                break;
            }


            try
            {
                foreach (IHTMLElement divElement in HTML.getElementsByTagName("h4"))
                {
                    string innerText = divElement.innerText;
                    if (innerText.Contains("에 대한 검색 결과가 없습니다."))
                    {
                        return null;
                    }

                }
            }
            catch (Exception e)
            {
                return null;
            }
            return HTML;
        }

        private void closeWebBrowser()
        {
            try
            {
                webBrowser.Quit();
            }
            catch (Exception ex){ }
            try
            {
                internetExplorer.Quit();
            }
            catch (Exception ex){ }
        }

        

        private void getPosDefInfor(HTMLDocument HTML, ArrayList wordInforSet)
        {
            try
            {
                foreach (IHTMLElement divElement in HTML.getElementsByTagName("ul"))
                {
                    string className = null;
                    try
                    {
                        className = divElement.getAttribute("className");
                    }
                    catch (Exception e)
                    {
                        continue;
                    }

                    if (className != null)
                    {
                        if (className.Equals("lst3"))
                        {
                            IHTMLElement2 divElementToElement = (IHTMLElement2)divElement;
                            foreach (IHTMLElement aTag in divElementToElement.getElementsByTagName("p"))
                            {
                                WordInfor tempWordInfo = new WordInfor();
                                string innerText = aTag.innerText;
                                int startIdx = innerText.IndexOf("[");
                                int endIdx = innerText.IndexOf("]");
                                int split = innerText.IndexOf(" ");
                                string pos = innerText.Substring(startIdx+1, endIdx-1);
                                string def = innerText.Substring(split+1);
                                tempWordInfo.POS = pos;
                                tempWordInfo.definitions = def;

                                wordInforSet.Add(tempWordInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return;
            }
        }

        private void getPOSDefinition(string url, WordInfor tempInfor)
        {
            HTMLDocument HTML = null;
            HTML = openWebBrowser(url);

            try
            {
                foreach (IHTMLElement divElement in HTML.getElementsByTagName("h4"))
                {
                    string className = null;
                    try
                    {
                        className = divElement.getAttribute("className");
                    }
                    catch (Exception e)
                    {
                        continue;
                    }

                    if (className != null)
                    {
                        if (className.Equals("class"))
                        {
                            string pos = divElement.innerText;
                            tempInfor.POS = pos;
                            break;
                        }
                    }
                }

                foreach (IHTMLElement divElement in HTML.getElementsByTagName("p"))
                {
                    string className = null;
                    try
                    {
                        className = divElement.getAttribute("className");
                    }
                    catch (Exception e)
                    {
                        continue;
                    }

                    if (className != null)
                    {
                        if (className.Equals("pclass"))
                        {
                            string def = divElement.innerText;
                            tempInfor.definitions = def;
                            break;
                        }
                    }
                }

            }
            catch (Exception e)
            {
                closeWebBrowser();
            }
            closeWebBrowser();
        }

        private void getDocURLs(HTMLDocument HTML, ArrayList docURLs)
        {
            try
            {
                foreach (IHTMLElement divElement in HTML.getElementsByTagName("ul"))
                {
                    string className = null;
                    try
                    {
                        className = divElement.getAttribute("className");
                    }
                    catch (Exception e)
                    {
                        continue;
                    }

                    if (className != null)
                    {
                        if (className.Equals("lst3"))
                        {
                            IHTMLElement2 divElementToElement = (IHTMLElement2)divElement;
                            foreach (IHTMLElement aTag in divElementToElement.getElementsByTagName("a"))
                            {
                                string tempURL = null;
                                try
                                {
                                    tempURL = aTag.getAttribute("href");
                                }
                                catch (Exception e)
                                {
                                    continue;
                                }

                                if (!docURLs.Contains(tempURL))
                                {
                                    docURLs.Add(tempURL);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            { 
                docURLs = null; 
            }
        }
    }
}
