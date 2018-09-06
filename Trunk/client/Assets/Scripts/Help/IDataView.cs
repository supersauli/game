
//数据视窗监听对象接口
public interface IDataView
{
    //视窗创建入口
    void on_create_view(string view_id, int capacity);
    //视窗删除入口
    void on_delete_view(string view_id);
    //视窗属性变化入口
    void on_view_property(string view_id, int count);
    //视窗添加对象入口
    void on_view_add(string view_id, string obj_index);
    //视窗删除对象入口
    void on_view_remove(string view_id, string obj_index);
    //视窗对象属性集变化入口(一般在第一次加载时回调到，其他时候不会有)
    void on_view_object_property(string view_id, string obj_index, int count);
    //视窗属性变化入口(单个属性变化)
    void on_view_object_property_change(string view_id, string obj_index, string name);
}
