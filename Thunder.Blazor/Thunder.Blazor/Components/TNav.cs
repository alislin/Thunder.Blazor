/* Ceated by Ya Lin. 2019/7/31 16:26:44 */

using System.Collections.Generic;
using System.Linq;

namespace Thunder.Blazor.Components
{
    public class TNav:TComponent<TNavContext>
    {

    }

    public class TNavContext : TContext
    {
        /// <summary>
        /// 导航子项目
        /// </summary>
        public List<TagBlockContext> NavItems { get; set; } = new List<TagBlockContext>();

        /// <summary>
        /// 当前激活的项目
        /// </summary>
        public TagBlockContext ActivedItem => NavItems?.FirstOrDefault(x => x.IsActived);

        public virtual void SetActice(TagBlockContext item)
        {
            NavItems.ForEach(x => x.IsActived = item.DomId == x.DomId);
            LoadDataContext();
        }
    }

}
