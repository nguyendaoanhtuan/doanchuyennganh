import 'package:flutter/material.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Flutter Demo',
      theme: ThemeData(
        colorScheme: ColorScheme.fromSeed(seedColor: Colors.deepPurple),
        useMaterial3: true,
      ),
      home: StackExample(),
    );
  }
}


// class ColumnExample extends StatelessWidget{
//   @override
//   Widget build(BuildContext context){
//     return Scaffold(
//       appBar: AppBar(
//         title: Text('Row Example'),
//       ),
//       body: Center(
//         child: Column(
//           mainAxisAlignment: MainAxisAlignment.center,
//           children: <Widget>[
//             Icon(Icons.star),
//             Icon(Icons.add),
//             Icon(Icons.tab)
//
//           ],
//         ),
//
//       ),
//       floatingActionButton: FloatingActionButton(
//         onPressed: (){},
//         child: Icon(Icons.add),
//       ),
//     );
//   }
class StackExample extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Row Example'),
      ),
      body: Center(
        child: Container(
          color: Colors.blueAccent,
          child: Padding(
            padding: EdgeInsets.all(16.0),
            child: Text(
              'Padded Text', style: TextStyle(color: Colors.white),),),


        ),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () {},
        child: Icon(Icons.add),
      ),
    );
  }
}
class StackExample extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Row Example'),
      ),
      body: Center(
        child: Container(
          color: Colors.blueAccent,
          child: Padding(
            padding: EdgeInsets.all(16.0),
            child: Text(
              'Padded Text', style: TextStyle(color: Colors.white),),),


        ),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () {},
        child: Icon(Icons.add),
      ),
    );
  }
}



