# QRLocationAR_Unity 📖  
> AR Library Navigation – Unity AR 연동 모듈

## 모듈 소개

QRLocationAR_Unity는 AR Library Navigation Android 애플리케이션과 연동되는 Unity 기반 AR 모듈입니다.  
Unity 환경에서 AR 콘텐츠를 제작하고 ARCore를 활용해 카메라 영상 위에 3D 화살표 오브젝트를 오버레이함으로써  
Android 앱에서 전달된 방향 안내 정보를 현실 공간 위에 시각적으로 표현합니다.

본 모듈은 독립적인 서비스가 아닌  
Android 애플리케이션의 기능 확장을 위한 AR 렌더링 전용 기술 컴포넌트로 설계되었습니다.

## 모듈 역할

- Android 앱에서 QR 기반 위치 정보 및 방향 데이터를 전달받아 AR 화면에 반영  
- Unity 기반 3D 화살표 AR 콘텐츠 렌더링  
- ARCore를 활용한 바닥 평면 인식 및 공간 추적  
- 카메라 영상 위에 안정적인 AR 오버레이 제공  
- Android–Unity 간 역할 분리를 고려한 연동 구조 설계

## 기술 스택

- Unity  
- C#  
- ARCore  
- 외부 3D 모델 라이브러리  
- Android–Unity 연동 구조  

## 기술적 의의

Unity를 활용해 AR 렌더링 영역을 분리하여,  
Android 환경에서 복잡한 3D 시각화와 ARCore 연동을 효율적으로 처리했습니다.

이를 통해 방향 안내를 보다 직관적으로 표현하고,  
AR 기능을 독립적인 모듈로 구성해 구조적 안정성과 확장성을 확보했습니다.

🔗 Main Application : QRLocationAR_Android

