import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_app/services/api_service.dart';
import 'package:flutter_app/widgets/features/images_grid_view/image_grid_view.dart';
import 'package:flutter_app/widgets/shared/bottom_navigation_bar_widget.dart';

class HomePage extends StatefulWidget {
  HomePage({Key? key, required this.title}) : super(key: key);

  final String title;

  @override
  _HomePageState createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  final apiService = ApiService();

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Scaffold(
        backgroundColor: Colors.white,
        appBar: AppBar(
          title: Text(widget.title),
        ),
        body: ImageGridViewsContainer(),
        bottomNavigationBar: BottomNavigationBarWidget(),
      ),
    );
  }
}
