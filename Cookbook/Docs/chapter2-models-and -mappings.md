# Chapter2.Models and Mappings

## 并发
乐观: 提交前进行检查。
悲观: 开始事物时进行锁定提交后结束锁定。
Nhibernate默认乐观?。当你使用session.Lock时才会是悲观。

## 映射方式
xml
code
约束（包含model inspector/model mapper）
fluently(NHF)

