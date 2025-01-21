using PlayFreely.HotfixRuntime;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEditor;
namespace PlayFreely.EditorTools
{
    /// <summary>
    /// 数据表更新
    /// </summary>
    public static partial class DataTableUpdater
    {
        /// <summary>
        /// 数据表
        /// </summary>
        private static IList<string> s_DataTableFileChangeList;

        /// <summary>
        /// 配置表
        /// </summary>
        private static IList<string> s_ConfigFileChangeList;

        /// <summary>
        /// 语言配置表
        /// </summary>
        private static IList<string> s_LanguageFileChangeList;

        /// <summary>
        /// 是否初始化
        /// </summary>
        private static bool m_IsInitialization = false;

        /// <summary>
        /// app热更配置
        /// </summary>
        private static AppHotfixRuntimeSettings m_HotfixAppConfige = null;

        /// <summary>
        /// 初始化
        /// </summary>
        [InitializeOnLoadMethod]
        private static void Initialization( )
        {
            if(m_IsInitialization)
            {
                return;
            }
            InitGlobalCulture( );
            s_DataTableFileChangeList = new List<string>( );
            s_ConfigFileChangeList = new List<string>( );
            s_LanguageFileChangeList = new List<string>( );

            EditorApplication.update -= OnUpdate;
            EditorApplication.update += OnUpdate;

            //数据表配置文件
            FileSystemWatcher dataWatcher = new FileSystemWatcher(PlayFreelyConstEditor.DataTableExcelPath , "*.xlsx");
            //当设置为 true 时，FileSystemWatcher 不仅会监视指定的目录，还会监视该目录下的所有子目录；当设置为 false 时，它只会监视直接指定的目录，而忽略其下的子目录
            dataWatcher.IncludeSubdirectories = true;
            //可以让你精确控制 FileSystemWatcher 对哪些文件系统更改进行通知
            dataWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName;
            //用于控制 FileSystemWatcher 是否开始监视文件系统的更改。
            //当 EnableRaisingEvents 被设置为 true 时，FileSystemWatcher 将开始监视指定路径（通过 Path 属性设置）下的文件和目录的变化，并在发生相应的事件时触发事件处理程序；
            //当它被设置为 false 时，FileSystemWatcher 会停止监视文件系统的变化，不会触发任何事件
            dataWatcher.EnableRaisingEvents = true;

            FileSystemEventHandler dataFileSystemEvent = new FileSystemEventHandler(OnDataTableChanged);
            RenamedEventHandler dataFileRenameEvent = new RenamedEventHandler(OnDataTableChanged);

            //这个事件在监视的文件或目录发生更改时被触发，它可以帮助你对文件系统中的文件更改进行实时监控和响应
            dataWatcher.Changed -= dataFileSystemEvent;
            dataWatcher.Changed += dataFileSystemEvent;

            //它会在被监视的文件或目录被删除时触发
            dataWatcher.Deleted -= dataFileSystemEvent;
            dataWatcher.Deleted += dataFileSystemEvent;

            //它在监视的文件或目录被重命名时触发
            dataWatcher.Renamed -= dataFileRenameEvent;
            dataWatcher.Renamed += dataFileRenameEvent;

            //配置表文件
            FileSystemWatcher configWatcher = new FileSystemWatcher(PlayFreelyConstEditor.ConfigeExcelPath , "*.xlsx");
            configWatcher.IncludeSubdirectories = true;
            configWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName;
            configWatcher.EnableRaisingEvents = true;
            FileSystemEventHandler configFileSystemEvent = new FileSystemEventHandler(OnConfigChanged);
            RenamedEventHandler configFileRenameEvent = new RenamedEventHandler(OnConfigChanged);

            configWatcher.Changed -= configFileSystemEvent;
            configWatcher.Changed += configFileSystemEvent;

            configWatcher.Deleted -= configFileSystemEvent;
            configWatcher.Deleted += configFileSystemEvent;

            configWatcher.Renamed -= configFileRenameEvent;
            configWatcher.Renamed += configFileRenameEvent;

            //本地化语言表配置文件
            FileSystemWatcher languageWatcher = new FileSystemWatcher(PlayFreelyConstEditor.ConfigeExcelPath , "*.xlsx");
            configWatcher.IncludeSubdirectories = true;
            configWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite;
            configWatcher.EnableRaisingEvents = true;
            FileSystemEventHandler languageFileSystemEvent = new FileSystemEventHandler(OnLanguageChanged);
            RenamedEventHandler languageFileRenameEvent = new RenamedEventHandler(OnLanguageChanged);

            languageWatcher.Changed -= languageFileSystemEvent;
            languageWatcher.Changed += languageFileSystemEvent;

            languageWatcher.Deleted -= languageFileSystemEvent;
            languageWatcher.Deleted += languageFileSystemEvent;

            languageWatcher.Renamed -= languageFileRenameEvent;
            languageWatcher.Renamed += languageFileRenameEvent;



            m_IsInitialization = true;
        }

        /// <summary>
        /// 初始化本地化信息配置
        /// <para>通过使用 CultureInfo.CreateSpecificCulture("en-GB")，可以为 Unity 应用程序添加对英国文化的本地化支持，确保应用程序在处理与英国相关的数据和显示信息时使用正确的文化信息</para>
        /// </summary>
        private static void InitGlobalCulture( )
        {
            CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
        }


        private static void OnUpdate( )
        {
            if(!m_IsInitialization)
            {
                return;
            }

        }


        /// <summary>
        /// 数据配置文件改变时回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnDataTableChanged(object sender , FileSystemEventArgs e)
        {
            string fileName = Path.GetFileNameWithoutExtension(e.Name);
            if(!fileName.StartsWith("~$"))
            {
                s_DataTableFileChangeList.Add(e.FullPath);
            }
        }

        /// <summary>
        /// 配置文件改变时回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnConfigChanged(object sender , FileSystemEventArgs e)
        {
            string fileName = Path.GetFileNameWithoutExtension(e.Name);
            if(!fileName.StartsWith("~$"))
            {
                s_ConfigFileChangeList.Add(e.FullPath);
            }
        }

        /// <summary>
        /// 本地化语言配置文件改变时回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnLanguageChanged(object sender , FileSystemEventArgs e)
        {
            string fileName = Path.GetFileNameWithoutExtension(e.Name);
            if(!fileName.StartsWith("~$"))
            {
                s_LanguageFileChangeList.Add(e.FullPath);
            }
        }


        /// <summary>
        /// 设置文件监听
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="filter">后缀</param>
        /// <param name="notify">哪些文件系统更改进行通知
        /// <para>Attributes：文件或目录的属性发生更改</para>
        /// <para>CreationTime：文件或目录的创建时间发生更改</para>
        /// <para>DirectoryName：目录的名称发生更改</para>
        /// <para>FileName：文件的名称发生更改</para>
        /// <para>LastAccess：文件或目录的最后访问时间发生更改</para>
        /// <para>LastWrite：文件或目录的最后写入时间发生更改</para>
        /// <para>Security：文件或目录的安全设置发生更改</para>
        /// <para>Size：文件或目录的大小发生更改</para>
        /// </param>
        /// <param name="fileSystem"></param>
        /// <param name="renamed"></param>
        private static void SetFileSystemWatcher(string filePath , string filter , NotifyFilters notify , FileSystemEventHandler fileSystem , RenamedEventHandler renamed)
        {
            FileSystemWatcher fileSystemWatcher = new FileSystemWatcher(filePath , filter);
            //当设置为 true 时，FileSystemWatcher 不仅会监视指定的目录，还会监视该目录下的所有子目录；当设置为 false 时，它只会监视直接指定的目录，而忽略其下的子目录
            fileSystemWatcher.IncludeSubdirectories = true;
            //可以让你精确控制 FileSystemWatcher 对哪些文件系统更改进行通知
            fileSystemWatcher.NotifyFilter = notify;
            //用于控制 FileSystemWatcher 是否开始监视文件系统的更改。
            //当 EnableRaisingEvents 被设置为 true 时，FileSystemWatcher 将开始监视指定路径（通过 Path 属性设置）下的文件和目录的变化，并在发生相应的事件时触发事件处理程序；
            //当它被设置为 false 时，FileSystemWatcher 会停止监视文件系统的变化，不会触发任何事件
            fileSystemWatcher.EnableRaisingEvents = true;

            FileSystemEventHandler fileChangeEH = new FileSystemEventHandler(fileSystem);


            //这个事件在监视的文件或目录发生更改时被触发，它可以帮助你对文件系统中的文件更改进行实时监控和响应
            fileSystemWatcher.Changed -= fileChangeEH;
            fileSystemWatcher.Changed += fileChangeEH;

            //它会在被监视的文件或目录被删除时触发
            fileSystemWatcher.Deleted -= fileChangeEH;
            fileSystemWatcher.Deleted += fileChangeEH;

            if(renamed != null)
            {
                RenamedEventHandler fileRenameEH = new RenamedEventHandler(renamed);

                //它在监视的文件或目录被重命名时触发
                fileSystemWatcher.Renamed -= fileRenameEH;
                fileSystemWatcher.Renamed += fileRenameEH;
            }
        }
    }
}
