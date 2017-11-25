#include <iostream>
#include <string>


using namespace std;

void calculaterec(int length,int width){
	cout<<"The area of the rectangle is :"<<length*width<<endl;
                             
							  }int main(){	
int reclength=0;
int recwidth=0;
int test[2];
cout<<"Please input the length of the rectangle:";
cin>>reclength;
cout<<"Please input the width of the rectangle:";
cin>>recwidth;
calculaterec(reclength,recwidth);
while(1){cout<<"haha";}
system("pause");
return 0;
          }