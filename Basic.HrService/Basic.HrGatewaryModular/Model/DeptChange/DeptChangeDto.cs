using Basic.HrRemoteModel.Dept.Model;
using Basic.HrRemoteModel.DeptChange.Model;

namespace Basic.HrGatewaryModular.Model.DeptChange
{
    public class DeptChangeDto
    {
        /// <summary>
        /// 变动受影响的部门
        /// </summary>
        public DeptTree[] ChangeDept { get; set; }

        public MergeEmpDto[] Emps { get; set; }
    }
}
