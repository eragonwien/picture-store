class FolderModel {
  String name;
  List<String> files = [];

  FolderModel();

  factory FolderModel.fromJson(Map<String, dynamic> data) {
    FolderModel model = new FolderModel();
    model.name = data['name'];
    var files = (data['files'] as List)?.map((e) => e as String)?.toList();
    model.files.addAll(files);
    return model;
  }
}
