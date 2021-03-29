import 'package:flutter/material.dart';
import 'package:pinch_zoom/pinch_zoom.dart';
import 'package:transparent_image/transparent_image.dart';

class ImageDialog extends StatefulWidget {
  final String image;

  const ImageDialog({Key key, @required this.image}) : super(key: key);

  @override
  ImageDialogState createState() => ImageDialogState(this.image);
}

class ImageDialogState extends State<ImageDialog> {
  String image;
  int currentIndex = 0;

  ImageDialogState(this.image) {
    this.image = image;
  }

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Scaffold(
        body: Container(
          child: PinchZoom(
            image: FadeInImage.memoryNetwork(
              placeholder: kTransparentImage,
              image: image,
            ),
            zoomedBackgroundColor: Colors.black.withOpacity(0.5),
            resetDuration: const Duration(milliseconds: 100),
            maxScale: 2.5,
          ),
        ),
        floatingActionButton: buildFloatingActionButton(),
      ),
    );
  }

  Widget buildFloatingActionButton() {
    return FloatingActionButton(
      onPressed: onFloatingActionButtonPressed,
      child: Icon(Icons.delete),
      backgroundColor: Colors.redAccent,
    );
  }

  void onFloatingActionButtonPressed() {
    FutureBuilder(
        future: showDeleteImageConfirmationDialog(),
        builder: (context, AsyncSnapshot snapshot) {
          print('show delete image confirmation dialog');
          return null;
        });
  }

  Future<void> showDeleteImageConfirmationDialog() async {
    return showDialog(
        context: context,
        barrierDismissible: false,
        builder: (BuildContext context) {
          return AlertDialog(
            actionsPadding: EdgeInsets.only(right: 5),
            content: Container(
              child: Text("Delete this ?"),
            ),
            actions: <Widget>[
              TextButton(
                child: Text('Confirm'),
                style: TextButton.styleFrom(
                    primary: Colors.red, backgroundColor: Colors.white),
                onPressed: () {
                  Navigator.of(context).pop();
                },
              ),
              TextButton(
                child: Text('Cancel'),
                onPressed: () {
                  Navigator.of(context).pop();
                },
              ),
            ],
          );
        });
  }
}
