﻿
using System;
using System.Collections.Generic;
using System.Text;
using ChakraCore.NET.API;
namespace ChakraCore.NET
{
    public partial class JSValueConverterService 
    {
        private readonly DateTime jsDateStart = new DateTime(1970, 1, 1);
        private void initDefault()
        {
            RegisterConverter<string>(
                (node, value) =>
                {
                    return node.WithContext<JavaScriptValue>(() =>
                    {
                        return JavaScriptValue.FromString(value);
                    });
                },
                (node, value) =>
                {
                    return node.WithContext<string>(() =>
                    {
                        return value.ConvertToString().ToString();
                    });
                }
                );
            this.RegisterArrayConverter<string>();

            this.RegisterConverter<int>(
                (node, value) =>
                {
                    return node.WithContext<JavaScriptValue>(() =>
                    {
                        return JavaScriptValue.FromInt32(value);
                    });
                },
                (node, value) =>
                {
                    return node.WithContext<int>(() =>
                    {
                        return value.ConvertToNumber().ToInt32();
                    });
                }
                );
            this.RegisterArrayConverter<int>();

            this.RegisterConverter<double>(
                (node, value) =>
                {
                    return node.WithContext<JavaScriptValue>(() =>
                    {
                        return JavaScriptValue.FromDouble(value);
                    });
                },
                (node, value) =>
                {
                    return node.WithContext<double>(() =>
                    {
                        return value.ConvertToNumber().ToDouble();
                    });
                }
                );
            this.RegisterArrayConverter<double>();

            this.RegisterConverter<bool>(
                (node, value) =>
                {
                    return node.WithContext<JavaScriptValue>(() =>
                    {
                        return JavaScriptValue.FromBoolean(value);
                    });
                },
                (node, value) =>
                {
                    return node.WithContext<bool>(() =>
                    {
                        return value.ConvertToBoolean().ToBoolean();
                    });
                }
                );
            this.RegisterArrayConverter<bool>();

            this.RegisterConverter<Single>(
                (node, value) =>
                {
                    return node.WithContext<JavaScriptValue>(() =>
                    {
                        return JavaScriptValue.FromDouble(value);
                    });
                },
                (node, value) =>
                {
                    return node.WithContext<Single>(() =>
                    {
                        return Convert.ToSingle(value.ConvertToNumber().ToDouble());
                    });
                }
                );
            this.RegisterArrayConverter<Single>();

            this.RegisterConverter<byte>(
                (node, value) =>
                {
                    return node.WithContext<JavaScriptValue>(() =>
                    {
                        return JavaScriptValue.FromDouble(Convert.ToDouble(value));
                    });
                },
                (node, value) =>
                {
                    return node.WithContext<byte>(() =>
                    {
                        return BitConverter.GetBytes(value.ToInt32())[0];
                    });
                }
                );
            this.RegisterArrayConverter<byte>();

            this.RegisterConverter<decimal>(
                (node, value) =>
                {
                    return node.WithContext<JavaScriptValue>(() =>
                    {
                        return JavaScriptValue.FromDouble(Convert.ToDouble(value));
                    });
                },
                (node, value) =>
                {
                    return node.WithContext<decimal>(() =>
                    {
                        return Convert.ToDecimal(value.ConvertToNumber().ToDouble());
                    });
                }
                );
            this.RegisterArrayConverter<decimal>();

            this.RegisterConverter<DateTime>(
                (node, value) =>
                {
                    //return node.WithContext<JavaScriptValue>(() =>
                    //{
                    //    return JavaScriptValue.FromDouble(Convert.ToDouble(value));
                    //});
                    return node.WithContext<JavaScriptValue>(() =>
                    {
                        var valueService = node.GetService<IJSValueService>();
                        var globalObject = valueService.JSGlobalObject;
                        JavaScriptPropertyId DateMethod = JavaScriptPropertyId.FromString("Date");
                        var constructor=globalObject.GetProperty(DateMethod);
                        return constructor.ConstructObject(globalObject, JavaScriptValue.FromDouble((value - jsDateStart).TotalMilliseconds));
                    });
                    //throw new NotImplementedException();
                },
                (node, value) =>
                {
                    return node.WithContext<DateTime>(() =>
                    {
                        return jsDateStart.AddMilliseconds(value.ConvertToNumber().ToDouble());
                    });
                }
                );
            this.RegisterArrayConverter<DateTime>();



            this.RegisterConverter<JSValue>(
                (node,value)=>
                {
                    return value.ReferenceValue;
                },
                (node,value)=>
                {
                    return new JSValue(node, value);
                }
                );

            this.RegisterMethodConverter();
            this.RegisterConverter<Guid>(
                (node, value) =>
                {
                    return node.WithContext<JavaScriptValue>(() =>
                    {
                        return JavaScriptValue.FromString(value.ToString());
                    });
                },
                (node, value) =>
                {
                    return node.WithContext<Guid>(() =>
                    {
                        return Guid.Parse(value.ConvertToString().ToString());
                    });
                }
                );

            #region Special Convert
            this.RegisterConverter<JavaScriptValue>(
                (node, value) =>
                {
                    return value;
                },
                (node, value) =>
                {
                    return value;
                }
                );
            this.RegisterArrayConverter<JavaScriptValue>();
            #endregion
        }
    }
}
