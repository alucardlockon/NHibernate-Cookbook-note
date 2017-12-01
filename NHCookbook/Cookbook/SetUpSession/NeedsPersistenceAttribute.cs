using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SetUpSession
{
    public class NeedsPersistenceAttribute : NHibernateSessionAttribute
    {
        protected ISession session
        {
            get
            {
                return sessionFactory.GetCurrentSession();
            }
        }

        public override void OnActionExecuting(
        ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            session.BeginTransaction();
        }

        public override void OnActionExecuted(
        ActionExecutedContext filterContext)
        {

            var tx = session.Transaction;
            if (tx != null && tx.IsActive)
            {
                var noUnhandledException =
                filterContext.Exception == null ||
                filterContext.ExceptionHandled;

                if (noUnhandledException &&
                filterContext.Controller.ViewData.ModelState.IsValid)
                {
                    session.Transaction.Commit();
                }
                else
                {
                    session.Transaction.Rollback();
                }
            }

            base.OnActionExecuted(filterContext);
        }
    }
}