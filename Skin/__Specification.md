# Skinプロジェクトについて
## 目的
- LR2Skinを読み込んで使えるようにする
- LR2Skinの上位互換となるようなC#ScriptSkinを作る

## 要求
- Skinクラスを持ち、各シーン毎にスキンクラスを持つ
  - .lr2Skinに該当
  - Optionにスキンのカスタムオプションを格納する
  - １個以上のDrawingクラスを持ち、オプションに応じて表示を変更する
- Drawingクラスで実際の描画を担当する
  - Skinクラスからカスタムオプションを読み込む
  - IMGリストとFontリストを持つ
  - 特殊定義の読み込みの実装
  - SRC/DST読み込みの実装
  - Timerをシーンからもらってアニメーションする
- DST option等に対応するクラスを作る
  - これはSceneで書き換えられて、PartsDrawingで読み込まれる

## 仕様
### Skin
- CustomOptionsはオプション名とオプションのセット
- スクリプト化するならJsonにする

### PartsDrawing
- スクリプト書くならPartsDrawing→(各継承先のクラス)をさらに継承して書く形
- パーツごとに分けられるようにするといいかも？だるそう

## 使用
### lr2skin
- Skin.LoadLr2Skinに投げ込んでおしまい！！！！
- ただし拡張的なことはあまりできない

### Script
- Skin→各Skinを継承して書く
```
public class WanisPlayAC:SkinBmsSp(:Skin)
{
    ~~
}

return new WanisPlayAC();
```
- globalsにInfoを継承したものを渡す
- 音楽はどういじる？
  - Sound()をSkinが持つかSceneが持つか