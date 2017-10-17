# Chapter1.The Configuration and Schema

## 1.Installing NHibernate 安装NHibernate
使用nuget图形化安装NHibernate。  
或者在vs的Package Manager Console中`Install-Package NHibernate -Project Eg.Core`。  

## 2.Configuring NHibernate with hibernate.cfg.xml 用hibernate.cfg.xml来配置NHibernate
1. 安装Nhibernate
2. 添加hibernate.cfg.xml到你的项目下。
hibernate.cfg.xml(oracle):  
```xml
<?xml version="1.0" encoding="utf-8"?>
<hibernate-configuration xmlns="urn:nhibernate-configuration-2.2">
    <session-factory>
        <property name="connection.driver_class">
            NHibernate.Driver.OracleClientDriver
        </property>
        <property name="dialect">
            NHibernate.Dialect.Oracle10gDialect
        </property>
        <property name="connection.connection_string">
            Data Source=orcl;User ID=dev_test;password=test123
        </property>
        <property name="adonet.batch_size">
            50
        </property>
    </session-factory>
</hibernate-configuration>
```
3. 右键hibernate.cfg.xml-属性:选择Copy if newer
4. 添加如下代码。
```c#
var nhConfig = new Configuration().Configure();
var sessionFactory = nhConfig.BuildSessionFactory();
Console.WriteLine("NHibernate Configured!");
Console.ReadKey();
```
5. 运行你的程序

* 批量处理已经试用于Microsoft SQL Server, Oracle, or MySQL。
* NHibernate默认寻找hibernate.cfg.xml文件。或者你能够用其他方式:
    * 使用不同的文件:
    ```c#
    var cfgFile = "cookbook.cfg.xml"; 
    var nhConfig = new Configuration().Configure(cfgFile);
    ```
    * 使用内嵌的文件:
    ```c#
    var assembly = GetType().Assembly;
    var path = "MyApp.cookbook.cfg.xml"; 
    var nhConfig = new Configuration().Configure(assembly, path);
    ```
    * 使用XmlReader:
    ```c#
    var doc = GetXmlDocumentWithConfig();
    var reader = new XmlNodeReader (doc);
    var nhConfig = new Configuration().Configure(reader);
    ```
* Oracle可用的Dialect:
    * Oracle12cDialect
    * Oracle10gDialect
    * Oracle9iDialect
    * Oracle8iDialect
    * OracleLiteDialect
* Oracle可用的Driver:
    * OracleClientDriver
    * OracleDataClientDriver
    * OracleLiteDataDriver
    * OracleManagedDataClientDriver
    

### NHibernate基础架构
                                                你的应用
                                                   |
配置(Builds session factory)->Session Factory(Builds sessions)->Session(工作单元模式(Unit of Work))
                                                   |
Dialect(方言)(创建sql语法) | Connection Provider(连接提供者)(打开或关闭连接) | Driver(驱动)(创建ADO.NET实体) | Batcher(批处理sql)

## 3.Configuring NHibernate with App.config or Web.config 用App.config或web.config来配置NHibernate
1. 打开App.config
2. 添加如下节点:
    ```xml
    <configSections>
    ? <section name="hibernate-configuration" type="NHibernate.Cfg.ConfigurationSectionHandler, 
    NHibernate" />
    </configSections>
    ```
3. 添加连接字符串:如
    ```xml
    <connectionStrings>
    ? <add name="db" connectionString="Server=.\SQLEXPRESS; Database=NHCookbook; Trusted_Connection=SSPI"/>
    </connectionStrings>
    ```
4. 添加你的hibernate-configuration节点
    ```xml
    <hibernate-configuration xmlns="urn:nhibernate-configuration-2.2">
        <session-factory>
            <property name="connection.driver_class">
                NHibernate.Driver.OracleClientDriver
            </property>
            <property name="dialect">
                NHibernate.Dialect.Oracle10gDialect
            </property>
            <property name="connection.connection_string">
                Data Source=orcl;User ID=dev_test;password=test123
            </property>
            <property name="adonet.batch_size">
                50
            </property>
        </session-factory>
    </hibernate-configuration>
    ```
5. 完整的app.config如下:
    ```xml
    <configuration>
    ? <configSections>
    ??? <section name="hibernate-configuration" type="NHibernate.Cfg.ConfigurationSectionHandler, NHibernate" />
    ? </configSections>
    ? <connectionStrings>
    ??? <add name="db" connectionString="Server=.\SQLEXPRESS; Database=NHCookbook; Trusted_Connection=SSPI" />
    ? </connectionStrings>
    <hibernate-configuration xmlns="urn:nhibernate-configuration-2.2">
    ? <session-factory>
    ??? <property name="dialect">
    ????? NHibernate.Dialect.MsSql2008Dialect, NHibernate
    ??? </property>
    ??? <property name="connection.connection_string_name">
    ????? db
    ??? </property>
    ??? <property name="adonet.batch_size">
    ????? 100
    ??? </property>
    ? </session-factory>
    </hibernate-configuration>
    </configuration>
    ```
6. 添加c#代码:
    ```c#
    var nhConfig = new Configuration().Configure();
    var sessionFactory = nhConfig.BuildSessionFactory();
    Console.WriteLine("NHibernate Configured!");
    Console.ReadKey();    
    ```
7. 运行

* Web.config配置同App.config

## 4.Configuring NHibernate with code 使用代码配置NHibernate
1. 在App.config下面添加如下代码:
    ```xml
    <configuration>
    ? <connectionStrings>
    ??? <add name="db" connectionString="Server=.\SQLEXPRESS; Database=NHCookbook; Trusted_Connection=SSPI" />
    ? </connectionStrings>
    </configuration>
    ```
2. 在Progarm.cs中添加引用:
    ```c#
    using NHibernate.Cfg;
    using NHibernate.Dialect;
    ```
3. 在main方法里添加如下代码:
    ```c#
    var nhConfig = new Configuration().DataBaseIntegration(db =>
    {
    ? db.Dialect<MsSql2012Dialect>();
    ? db.ConnectionStringName = "db";
    ? db.BatchSize = 100;
    });
    var sessionFactory = nhConfig.BuildSessionFactory();
    Console.WriteLine("NHibernate Configured!");
    Console.ReadKey();
    ```
4. 运行。

## 5.Configuring NHibernate with Fluent NHibernate 使用Fluent NHibernate来配置NHibernate(第三方)
第三方库Fluent NHibernate拥有自己的语法来配置NHibernate。  
1. 在app.config文件配置
   ```xml
   <configuration>
   ? <connectionStrings>
   ??? <add name="db" connectionString="Server=.\SQLEXPRESS; Database=NHCookbook; Trusted_Connection=SSPI" />
   ? </connectionStrings>
   </configuration>
   ```
2. 在Program.cs中添加引用:
    ```c#
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    ```
3. 在main方法里添加如下代码:
    ```c#
    var config = MsSqlConfiguration.MsSql2012
    ? .ConnectionString(connstr => connstr.FromConnectionStringWithKey("db"))
    ? .AdoNetBatchSize(100);
    var nhConfig = Fluently.Configure()
    ? .Database(config)
    ? .BuildConfiguration();
    var sessionFactory = nhConfig.BuildSessionFactory();
    Console.WriteLine("NHibernate configured fluently!");
    Console.ReadKey();
    ```
4. 运行

## 6.Configuring NHibernate logging 配置NHibernate日志
NHibernate提供了一个log4net的日志提供器。
1. nuget安装log4net
2. 打开config文件(app or web.config)
3. 添加log4net节点:
    ```xml
    <section name="log4net"
    type="log4net.Config.Log4NetConfigurationSectionHandler, log4net"/>
    ```
4. 添加log4net配置:
    ```xml
    <log4net>
    <appender name="trace" type="log4net.Appender.TraceAppender, log4net">
    ? <layout type="log4net.Layout.PatternLayout, log4net">
    ? <param name="ConversionPattern"  value=" %date %level %message%newline" />
    ? </layout>
    </appender>
    <root>
    ? <level value="ALL" />
    ? <appender-ref ref="trace" />
    </root>
    <logger name="NHibernate">
    ? <level value="INFO" />
    </logger>
    </log4net>
    ```
5. 在main方法的最前面加入:
    ```c#
    log4net.Config.XmlConfigurator.Configure();
    ```
6. 运行，观察你的debug窗口

* 可以使用下面的方法输出日志:
    ```c#
    using System.IO;
    using log4net;
    namespace MyApp.Project.SomeNamespace
    {
    ??? public class Foo
    ??? {
    ??????? private static ILog log = LogManager.GetLogger(typeof(Foo));
    
    ??????? public string DoSomething()
    ??????? {
    ??????????? log.Debug("We're doing something.");
    ??????????? try
    ??????????? {
    ??????????????? return File.ReadAllText("cheese.txt");
    ??????????? }
    ??????????? catch (FileNotFoundException)
    ??????????? {
    ??????????????? log.Error("Somebody moved my cheese.txt");
    ??????????????? throw;
    ??????????? }
    ??????? }
    ??? }
    }
    ```

## 7.Generating the database 生成数据库
1. 打开Program.cs
2. 添加引用:
    ```c#
    using Eg.Core;
    using NHibernate.Mapping.ByCode; 
    using NHibernate.Tool.hbm2ddl;
    ```
3. 编辑Main方法:
    ```c#
    var nhConfig = new Configuration().Configure();
    var mapper=new ConventionModelMapper();
    nhConfig.AddMapping(mapper.CompileMappingFor(new[] {typeof (TestClass)}));
    
    var schemaExport = new SchemaExport(nhConfig);
    schemaExport.Create(false, true);
    
    Console.WriteLine("The tables have been created"));
    Console.ReadKey();
    ```
4. 运行，观察你的数据库是否有表建好了。

* 可使用update，create,create-drop,validate四种模式创建数据库。

## 8.Scripting the database 生成数据库脚本
1. 打开Program.cs
2. 添加引用:
    ```c#
    using Eg.Core;
    using NHibernate.Mapping.ByCode; 
    using NHibernate.Tool.hbm2ddl;
    ```
3. 编辑Main方法:
    ```c#
    var nhConfig = new Configuration().Configure();
    var mapper = new ConventionModelMapper();
    nhConfig.AddMapping(mapper.CompileMappingFor(new[] { typeof(TestClass) }));
    
    var schemaExport = new SchemaExport(nhConfig);
    schemaExport
    ??? .SetOutputFile(@"db.sql")
    ??? .Execute(false, false, false);
    
    Console.WriteLine("An sql file has been generated at {0}",
    ????????????????? Path.GetFullPath("db.sql"));
    Console.ReadKey();
    ```
4. 运行，查看你的db.sql文件。

## 9.Updating the database 生成更新数据库的脚本
1. 打开Program.cs
2. 添加引用:
    ```c#
    using Eg.Core;
    using NHibernate.Mapping.ByCode; 
    using NHibernate.Tool.hbm2ddl;
    ```
3. 编辑Main方法:
    ```c#
    var nhConfig = new Configuration().Configure();
    var mapper = new ConventionModelMapper();
    nhConfig.AddMapping(mapper.CompileMappingFor(new[] { typeof(TestClass) }));
    var update = new SchemaUpdate(nhConfig);
    update.Execute(false, true);
    Console.WriteLine("The tables have been updated");
    Console.ReadKey();
    ```
4. 运行，查看数据库中的表。
5. 修改TestClass的属性:
    ```c#
    public virtual string Description { get; set; }
    ```
6. 再次运行，看是否新增了一列Description。

## 10.Using NHibernate schema tool 使用NHibernate Schema Tool
下载最新的NHibernate Schema Tool。(http://nst.codeplex.com/)

安装步骤:  
1. 在C:\Program Files创建一个新文件夹NHibernateSchemaTool。
2. 复制nst.exe到文件夹.
3. 添加C:\Program Files\NHibernateSchemaTool到Path中.

使用步骤:
1. 编译
2. 打开命令行至hibernate.cfg.xml所在目录。
3. 运行命令:
    ```bash
    nst /c:hibernate.cfg.xml /a:Eg
    .Core.dll /o:Create.
    ```
