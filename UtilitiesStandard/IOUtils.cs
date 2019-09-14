using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Utilities;
using Utilities.Extensions;
using Utilities.Types;

namespace Utilities
{
    //public static class IOUtils
    //{   
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="filter">Пример: PDF (*.pdf)|*.pdf</param>
    //    /// <returns></returns>
    //    public static string RequestFileOpenPath(string filter)
    //    {
    //        var dialog = new OpenFileDialog();
    //        dialog.Filter = filter;
    //        dialog.AddExtension = true;
    //        dialog.RestoreDirectory = true;

    //        return dialog.ShowDialog() == DialogResult.OK 
    //            ? dialog.FileName 
    //            : null;
    //    }

    //    public static string RequestDirectoryOpenPath()
    //    {
    //        var dialog = new FolderBrowserDialog();

    //        return dialog.ShowDialog() == DialogResult.OK 
    //            ? dialog.SelectedPath 
    //            : null;
    //    }

    //    public static string RequestDirectorySavingPath()
    //    {
    //        var dialog = new FolderBrowserDialog();

    //        return dialog.ShowDialog() == DialogResult.OK 
    //            ? dialog.SelectedPath 
    //            : null;
    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="filter">Пример: PDF (*.pdf)|*.pdf</param>
    //    /// <returns></returns>
    //    public static string RequestFileSavingPath(string filter)
    //    {
    //        var dialog = new SaveFileDialog();
    //        dialog.Filter = filter;
    //        dialog.AddExtension = true;
    //        dialog.RestoreDirectory = true;

    //        return dialog.ShowDialog() == DialogResult.OK 
    //            ? dialog.FileName 
    //            : null;
    //    }

    //    public static string RequestFolderPath()
    //    {
    //        var dialog = new FolderBrowserDialog();

    //        return dialog.ShowDialog() == DialogResult.OK 
    //            ? dialog.SelectedPath 
    //            : null;
    //    }

    //    /// <summary>
    //    /// Создает файл или перезаписывает существующий.
    //    /// При необходимости создает директорию, в которой лежит файл.
    //    /// </summary>
    //    /// <param name="filePath"></param>
    //    public static FileStream TryCreateFileOrNull(string filePath)
    //    {
    //        TryCreateDirectoryIfNotExist(Path.GetDirectoryName(filePath));
    //        return CommonUtils.TryOrDefault(() => File.Create(filePath), null);
    //    }
    //    /// <summary>
    //    /// Создает файл или перезаписывает существующий.
    //    /// При необходимости создает директорию, в которой лежит файл.
    //    /// </summary>
    //    /// <param name="filePath"></param>
    //    public static FileStream CreateFile(string filePath)
    //    {
    //        return CreateFile(filePath, FileAccess.ReadWrite, FileShare.None);
    //        //CreateDirectoryIfNotExist(Path.GetDirectoryName(filePath));
    //        //return File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
    //        //return File.Create(filePath).FileS;
    //    }

    //    public static FileStream CreateFile(string filePath, FileAccess fileAccess, FileShare fileShare)
    //    {
    //        CreateDirectoryIfNotExist(Path.GetDirectoryName(filePath));
    //        return File.Open(filePath, FileMode.Create, fileAccess, fileShare);
    //    }

    //    /// <summary>
    //    /// Создает файл или перезаписывает существующий. Если файл успешно создан, записывает массив <see cref="data"/>. 
    //    /// При необходимости создает директорию, в которой лежит файл.
    //    /// </summary>
    //    /// <param name="filePath"></param>
    //    /// <param name="data"></param>
    //    /// <returns></returns>
    //    public static FileStream TryCreateFileOrNull(string filePath, byte[] data)
    //    {
    //        TryCreateDirectoryIfNotExist(Path.GetDirectoryName(filePath));
    //        var file = CommonUtils.TryOrDefault(() => new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read), null);
    //        if (file != null)
    //        {
    //            file.Write(data, 0, data.Length);
    //            file.Flush();
    //            file.Position = 0;
    //        }

    //        return file;
    //    }

    //    /// <summary>
    //    /// Создает файл или открывает существующий в режиме FileAccess.ReadWrite. Если файл успешно СОЗДАН(!), записывает массив <see cref="data"/>. При необходимости создает директорию, в которой лежит файл.
    //    /// </summary>
    //    /// <param name="filePath"></param>
    //    /// <param name="initialData"></param>
    //    /// <returns></returns>
    //    public static FileStream TryCreateFileIfNotExistOrOpenOrNull(string filePath)
    //    {
    //        return TryCreateFileIfNotExistOrOpenOrNull(filePath, new byte[0]);
    //    }
    //    /// <summary>
    //    /// Создает файл или открывает существующий в режиме FileAccess.ReadWrite. Если файл успешно СОЗДАН(!), записывает массив <see cref="data"/>. При необходимости создает директорию, в которой лежит файл.
    //    /// </summary>
    //    /// <param name="filePath"></param>
    //    /// <param name="initialData"></param>
    //    /// <returns></returns>
    //    public static FileStream TryCreateFileIfNotExistOrOpenOrNull(string filePath, byte[] initialData)
    //    {
    //        TryCreateDirectoryIfNotExist(Path.GetDirectoryName(filePath));
    //        if (File.Exists(filePath))
    //        {
    //            return CommonUtils
    //                .TryOrDefault(() => File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.Read), null);
    //        }
    //        else
    //        {
    //            return TryCreateFileOrNull(filePath, initialData);
    //        }
    //    }

    //    public static void DeleteFile(string filePath)
    //    {
    //        if (File.Exists(filePath))
    //        {
    //            File.Delete(filePath);
    //        }
    //    }
    //    public static bool TryDeleteFile(string filePath)
    //    {
    //        if (!File.Exists(filePath))
    //        {
    //            return true;
    //        }

    //        return CommonUtils.Try(() => File.Delete(filePath));
    //    }
    //    public static bool TryDeleteAllFilesInDirectory(string dirPath)
    //    {
    //        bool isOk = false;
    //        var files = CommonUtils.TryOrDefault(() => EnumerateFilesOrCreateOrNull(dirPath)?.ToArray());
    //        if (files != null)
    //        {
    //            isOk = true;
    //            foreach (var file in files)
    //            {
    //                isOk &= TryDeleteFile(file);
    //            }
    //        }

    //        return isOk;
    //    }
    //    public static void RecreateDirectory(string dirPath)
    //    {
    //        if (Directory.Exists(dirPath))
    //        {
    //            Directory.Delete(dirPath, true);
    //        }
    //        Directory.CreateDirectory(dirPath);
    //    }

    //    public static bool TryCreateDirectory(string dirPath)
    //    {
    //        return TryCreateDirectory(dirPath, out Exception ex);
    //    }
    //    public static bool TryCreateDirectory(string dirPath, out Exception exception)
    //    {
    //        if (Directory.Exists(dirPath))
    //        {
    //            exception = null;
    //            return true;
    //        }

    //        return CommonUtils.Try(() => Directory.CreateDirectory(dirPath), out exception);
    //    }

    //    public static bool IsFilePathValid(string filePath)
    //    {
    //        // https://stackoverflow.com/questions/422090/in-c-sharp-check-that-filename-is-possibly-valid-not-that-it-exists

    //        System.IO.FileInfo fi = null;
    //        try
    //        {
    //            fi = new System.IO.FileInfo(filePath);
    //        }
    //        catch (ArgumentException) { }
    //        catch (System.IO.PathTooLongException) { }
    //        catch (NotSupportedException) { }
    //        if (ReferenceEquals(fi, null))
    //        {
    //            // file name is not valid
    //            return false;
    //        }
    //        else
    //        {
    //            // file name is valid... May check for existence by calling fi.Exists.
    //            return true;
    //        }
    //    }

    //    public static void CreateDirectoryIfNotExist(params string[] directory)
    //    {
    //        foreach (string dir in directory)
    //            if (!Directory.Exists(dir))
    //                Directory.CreateDirectory(dir);
    //    }
    //    public static bool TryCreateDirectoryIfNotExist(string directory)
    //    {
    //        if (!Directory.Exists(directory))
    //        {
    //            return TryCreateDirectory(directory);
    //        }
    //        else
    //        {
    //            return true;
    //        }
    //    }
    //    public static IEnumerable<string> EnumerateFilesOrCreateOrNull(string directory)
    //    {
    //        bool exist = TryCreateDirectoryIfNotExist(directory);
    //        if (exist)
    //        {
    //            return Directory.EnumerateFiles(directory);
    //        }
    //        else
    //        {
    //            return null;
    //        }
    //    }
    //    public static IEnumerable<string> EnumerateFilesOrNull(string directory)
    //    {
    //        if (!Directory.Exists(directory))
    //        {
    //            return null;
    //        }
    //        else
    //        {
    //            return Directory.EnumerateFiles(directory);
    //        }
    //    }
    //}
}
