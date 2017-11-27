#include <iostream>
#include <string>


using namespace std;

void calculaterec(int length,int width){
	cout<<"The area of the rectangle is :"<<length*width<<endl;
                             
							  }int main(){	
int reclength=0;
int recwidth=0;
int x=0;
int s=5/x;
cout<<"Please input the length of the rectangle:";
cin>>reclength;
cout<<"Please input the width of the rectangle:";
cin>>recwidth;
calculaterec(reclength,recwidth);

return 0;
          }