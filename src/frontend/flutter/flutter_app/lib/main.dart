import 'dart:io';

import 'package:flutter/material.dart';
import 'package:flutter_app/helpers/http_overrides.dart';
import 'package:flutter_app/widgets/home.dart';

void main() {
  HttpOverrides.global = new AppHttpOverrides();
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Picture Store',
      theme: ThemeData(
        primarySwatch: Colors.orange,
      ),
      home: HomePage(title: 'Flutter Demo Home Page'),
    );
  }
}
