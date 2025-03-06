using Basic.HrRemoteModel.Emp.Model;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.Model;
namespace Basic.HrGatewaryModular.Model.Emp
{
    /// <summary>
    /// 查询人员信息 UI参数实体
    /// </summary>
    internal class UI_QueryEmp : BasicPage
    {
        /// <summary>
        /// 人员查询参数
        /// </summary>
        [NullValidate("public.query.param.null")]
        public EmpQuery Query { get; set; }

    }
}
