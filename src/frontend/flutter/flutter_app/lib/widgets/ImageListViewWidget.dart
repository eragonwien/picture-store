import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_app/models/ImageFolderModel.dart';
import 'package:flutter_app/services/apiService.dart';

class ImageGridViewFutureContainer extends StatefulWidget {
  @override
  ImageFutureWrapperState createState() => ImageFutureWrapperState();
}

class ImageFutureWrapperState extends State<ImageGridViewFutureContainer> {
  bool isLoading = false;
  final folderCount = 1;
  String startPage = '';
  Map<String, List<String>> data = Map<String, List<String>>();
  final apiService = new ApiService();

  @override
  Widget build(BuildContext context) {
    return FutureBuilder(
      future: apiService.listFiles(),
      builder: (context, AsyncSnapshot<List<FolderModel>> snapshot) {
        if (!snapshot.hasData) return Center(child: Text("Loading ..."));

        return buildImageGridView(snapshot.data);
      },
    );
  }
}

Widget buildImageGridView(List<FolderModel> data) {
  final keys = data.map((e) => e.name).toList();

  return Column(
    children: [
      Expanded(
        child: SafeArea(
          top: false,
          bottom: false,
          child: GridView.builder(
            gridDelegate: SliverGridDelegateWithMaxCrossAxisExtent(
                maxCrossAxisExtent: 200),
            itemBuilder: (context, index) {
              if (keys.length <= index) return Container();

              final itemData = data.singleWhere((e) => e.name == keys[index]);

              if (itemData == null) return Container();

              return buildImageGridViewItem(itemData);
            },
          ),
        ),
      )
    ],
  );
}
