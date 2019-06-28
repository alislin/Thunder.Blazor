using System.Collections.Generic;

namespace Thunder.Blazor.Components
{
    /// <summary>
    /// 级联参数类
    /// </summary>
    public class ComponentParamenter
    {
        /// <summary>
        /// 参数字典
        /// </summary>
        private Dictionary<string, object> parameters;

        public ComponentParamenter()
        {
            parameters = new Dictionary<string, object>();
        }

        public ComponentParamenter(string parameterName, object value)
        {
            parameters = new Dictionary<string, object>();
            Add(parameterName, value);
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="parameterName">名称</param>
        /// <param name="value">参数</param>
        public void Add(string parameterName, object value)
        {
            parameters[parameterName] = value;
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="parameterName">名称</param>
        /// <returns></returns>
        public T Get<T>(string parameterName)
        {
            if (!parameters.ContainsKey(parameterName))
            {
                throw new KeyNotFoundException($"{parameterName} 参数不存在");
            }

            return (T)parameters[parameterName];
        }
    }


}
