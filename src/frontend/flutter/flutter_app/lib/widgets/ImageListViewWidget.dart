import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_app/services/apiService.dart';

class ImageGridViewsContainer extends StatelessWidget {
  final apiService = new ApiService();

  @override
  Widget build(BuildContext context) {
    return Container(
      child: FutureBuilder(
        future: apiService.listFiles(),
        builder: (context, AsyncSnapshot<Map<String, List<String>>> snapshot) {
          if (!snapshot.hasData) return Center(child: Text("Loading ..."));

          return _buildImageGridViews(snapshot.data);
        },
      ),
    );
  }
}

Widget _buildImageGridViews(Map<String, List<String>> data) {
  return ListView.builder(
    shrinkWrap: true,
    itemCount: data.keys.length,
    itemBuilder: (BuildContext context, int index) {
      if (index >= data.keys.length) return null;

      var key = data.keys.elementAt(index);

      return Column(
        children: [
          Container(
            height: 50,
            color: Colors.greenAccent,
            child: Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                Text(key),
              ],
            ),
          ),
          Container(
            child: GridView.count(
              shrinkWrap: true,
              physics: BouncingScrollPhysics(),
              crossAxisCount: 2,
              crossAxisSpacing: 10,
              mainAxisSpacing: 10,
              children: _buildImageGridViewChildren(key, data[key]),
            ),
          ),
        ],
      );
    },
  );
}

List<Widget> _buildImageGridViewChildren(
    String folder, List<String> imageSources) {
  return imageSources
      .map(
        (imageSource) => GridTile(
          child: Container(
            alignment: Alignment.center,
            child: FittedBox(
              child: Image.network(imageSource),
              fit: BoxFit.fill,
            ),
            color: Colors.lightGreen,
          ),
        ),
      )
      .toList();
}

Widget _buildImageGridViewChild(String folder, String imageSource) {
  return GridTile(
    child: Image.network(imageSource),
  );
}

// Widget _buildImageGridView(List<String> data) {
//   return GridView.builder(
//     padding: const EdgeInsets.all(20),
//     gridDelegate: SliverGridDelegateWithMaxCrossAxisExtent(
//         maxCrossAxisExtent: 500.0, crossAxisSpacing: 20, mainAxisSpacing: 20),
//     itemCount: data.length,
//     itemBuilder: (BuildContext context, int index) {
//       if (data.length <= index) return null;

//       return Container(
//         alignment: Alignment.center,
//         padding: const EdgeInsets.all(8),
//         child: buildImageGridViewCardItem(data[index]),
//       );
//     },
//   );
// }
