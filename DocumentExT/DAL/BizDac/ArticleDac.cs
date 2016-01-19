using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Makersn.Models;
using NHibernate.Criterion.Lambda;
using Makersn.Util;

namespace Makersn.BizDac
{
    public class ArticleDac : BizDacHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleNo"></param>
        /// <param name="visiteNo"></param>
        /// <returns></returns>
        public ArticleDetailT GetArticleDetailByArticleNo(int articleNo, int visiteNo)
        {
            string query = @"select 
	                                        A.[NO], A.MEMBER_NO, A.MAIN_IMAGE,  A.CODE_NO,
											(SELECT TOP 1 TD.TITLE FROM TRANSLATION_DETAIL TD WITH(NOLOCK) WHERE TD.ARTICLE_NO = A.NO ORDER BY NO ASC ) AS TITLE, 
											(SELECT TOP 1 TD.CONTENTS FROM TRANSLATION_DETAIL TD WITH(NOLOCK) WHERE TD.ARTICLE_NO = A.NO ORDER BY NO ASC ) AS CONTENTS,
											(SELECT TOP 1 TD.TAG FROM TRANSLATION_DETAIL TD WITH(NOLOCK) WHERE TD.ARTICLE_NO = A.NO ORDER BY NO ASC ) AS TAG,
                                            A.COPYRIGHT, A.VISIBILITY, A.VIEWCNT, A.TEMP, A.REG_IP,
	                                        A.REG_DT, A.REG_ID, A.RECOMMEND_YN, A.RECOMMEND_DT, M.NAME as MEMBER_NAME, M.PROFILE_PIC as MEMBER_PROFILE_PIC, 
											case F.FILE_TYPE when 'img' then F.RENAME else F.IMG_NAME end as MAINIMGNAME, 
	                                        (SELECT COUNT(0) FROM LIKES WHERE ARTICLE_NO = A.[NO]) AS LIKE_CNT,
	                                        (SELECT COUNT(0) FROM ARTICLE_COMMENT WHERE ARTICLE_NO = A.[NO]) AS COMMENT_CNT,
	                                        (SELECT COUNT(0) FROM LIKES WHERE ARTICLE_NO = A.[NO] AND MEMBER_NO = :visiteNo) AS IS_LIKES, '0' as UPLOAD_CNT, '0' as DRAFT_CNT,
                                            A.VIDEO_URL 
                                        from ARTICLE A with(nolock)
                                        inner join MEMBER M with(nolock) on M.[NO] = A.MEMBER_NO
                                        inner join ARTICLE_FILE F with(nolock) on F.[NO] = A.MAIN_IMAGE
                                        where A.[NO] = :articleNo";
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IQuery queryObj = session.CreateSQLQuery(query).AddEntity(typeof(ArticleDetailT));
                queryObj.SetParameter("articleNo", articleNo);
                queryObj.SetParameter("visiteNo", visiteNo);

                ArticleDetailT detailT = queryObj.UniqueResult<ArticleDetailT>();

                session.Flush();

                return detailT;
            }
        }
    }
}
