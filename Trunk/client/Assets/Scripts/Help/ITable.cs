using SysUtils;

//虚拟数据表监听对象接口
public interface ITable
{
    //startIndex：： 数据起始索引
    //虚拟表添加数据入口
    void on_table_add(string tableName, VarList args, int iRows, int Cols, int startIndex);
    //虚拟表修改数据(某列)入口
    void on_table_change_single(string tableName, VarList args, int iRow, int Col, int Rows, int Cols, int startIndex);
    //虚拟表修改数据入口
    void on_table_change(string tableName, VarList args, int iRow, int Rows, int Cols, int startIndex);
    //虚拟表删除数据入口
    void on_table_delete(string tableName, VarList args, int iRow, int Rows, int Cols, int startIndex);
    //虚拟表清空数据入口
    void on_table_clear(string tableName, VarList args, int startIndex);
}

