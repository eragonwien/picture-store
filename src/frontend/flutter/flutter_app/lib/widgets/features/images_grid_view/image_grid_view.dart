import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_app/services/api_service.dart';
import 'package:transparent_image/transparent_image.dart';
import 'package:flutter_staggered_grid_view/flutter_staggered_grid_view.dart';
import 'image_dialog.dart';

class ImageGridViewsContainer extends StatefulWidget {
  @override
  ImageGridViewsContainerState createState() => ImageGridViewsContainerState();
}

class ImageGridViewsContainerState extends State<ImageGridViewsContainer> {
  final apiService = new ApiService();

  List<String> images = [];
  String? selectedImage;

  @override
  Widget build(BuildContext context) {
    return Container(
      child: FutureBuilder(
        future: apiService.listFiles(),
        builder: (BuildContext context, AsyncSnapshot<List<String>> snapshot) {
          if (snapshot.hasError) return Container();

          if (!snapshot.hasData)
            return Center(child: CircularProgressIndicator());

          images = snapshot.data!.toList();

          final orientation = MediaQuery.of(context).orientation;

          return buildStagerredGridView(images, orientation);
        },
      ),
    );
  }

  Widget buildStagerredGridView(List<String> data, Orientation orientation) {
    return Container(
      margin: EdgeInsets.all(16),
      child: StaggeredGridView.countBuilder(
        crossAxisCount: 2,
        crossAxisSpacing: 16,
        mainAxisSpacing: 16,
        itemCount: data.length,
        itemBuilder: (BuildContext context, int index) {
          if (data.length <= index) return Container();

          String image = data[index];

          return Container(
            child: ClipRRect(
              borderRadius: BorderRadius.circular(15),
              child: GestureDetector(
                onTap: () => onImageTileTapped(context, image),
                child: FadeInImage.memoryNetwork(
                  image: image,
                  placeholder: kTransparentImage,
                  fit: BoxFit.cover,
                ),
              ),
            ),
            decoration: BoxDecoration(
                color: Colors.transparent,
                border: Border.all(color: Colors.orange),
                borderRadius: BorderRadius.all(Radius.circular(15))),
          );
        },
        staggeredTileBuilder: (int index) =>
            new StaggeredTile.count(1, index.isEven ? 1.2 : 1.8),
      ),
    );
  }

  onImageTileTapped(BuildContext context, String image) {
    selectedImage = image;
    Navigator.of(context)
        .push(
          MaterialPageRoute(
              builder: (BuildContext context) {
                return ImageDialog(image: image);
              },
              fullscreenDialog: true),
        )
        .then((value) => onImageDialogClosed(value));
  }

  void onImageDialogClosed(value) {
    bool isDeleted = value != null && value as bool;

    if (!isDeleted) return;

    triggerStateChange();

    selectedImage = null;
  }

  void triggerStateChange() {
    setState(() {});
  }
}
