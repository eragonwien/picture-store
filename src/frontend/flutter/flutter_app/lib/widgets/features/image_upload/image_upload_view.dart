import 'package:dotted_border/dotted_border.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';

class ImageUploadPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Container(
      padding: const EdgeInsets.all(8),
      child: ListView.builder(
        itemBuilder: buildListView,
      ),
    );
  }

  Widget buildListView(BuildContext context, int index) {
    if (index == 0) return buildFileInput();

    if (index % 2 == 0)
      return Container(
        padding: const EdgeInsets.all(8),
        color: Colors.blue,
        height: 100,
        child: Container(
          child: Container(
            padding: const EdgeInsets.all(8),
            color: Colors.deepOrange,
            height: 100,
          ),
        ),
      );

    return Container(
      padding: const EdgeInsets.all(8),
      color: Colors.deepOrange,
      height: 100,
      child: Container(
        child: Container(
          padding: const EdgeInsets.all(8),
          color: Colors.blue,
          height: 100,
        ),
      ),
    );
  }

  Widget buildFileInput() {
    return Container(
      padding: const EdgeInsets.all(8),
      child: DottedBorder(
        color: Colors.black,
        strokeWidth: 1,
        child: Container(
          color: Colors.lightBlue,
          height: 100,
        ),
      ),
    );
  }

  Widget buildUploadingProgresses() {
    return ListView.builder(
      itemBuilder: buildListView,
    );
  }
}
