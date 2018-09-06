
//可视表监听对象接口
public interface IRecord
{
    //可视数据表结构入口(一般在游戏刚进入场景时就处理过了)
    void on_record_table(int nRecordCount);
    //可视数据表添加行入口
    void on_record_add_row(string strRecordName, int nRow, int nRows);
    //可视数据表删除行入口
    void on_record_remove_row(string strRecordName, int nRow);
    //可视数据表删除行入口[数据真正删除之前]
    void on_record_remove_row_before(string strRecordName, int nRow);

    //可视数据表改变一行数据入口
    void on_record_grid(string strRecordName, int nCount);
    //可视数据表改变一行中的分列入口
    void on_record_single_grid(string strRecordName, int nRow, int nCol);
    //可视数据表删除入口
    void on_record_clear(string strRecordName);
    /// 注册时
    //void on_record_regist(string strRecordName, int nRows);
}

