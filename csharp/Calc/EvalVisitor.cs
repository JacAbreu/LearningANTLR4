using System;
using System.Collections.Generic;

namespace Calc
{
    class EvalVisitor : LabeledExprBaseVisitor<double>
    {
        readonly Dictionary<string, double> _memory = 
            new Dictionary<string, double>();

        public override double VisitAssign(LabeledExprParser.AssignContext context)
        {
            var id = context.ID().GetText();
            var value = Visit(context.expr());
            _memory[id] = value;
            return value;
        }

        public override double VisitPrintExpr(LabeledExprParser.PrintExprContext context)
        {
            var value = Visit(context.expr());
            Console.WriteLine(value);
            return 0;
        }

        public override double VisitInt(LabeledExprParser.IntContext context)
        {
            return double.Parse(context.INT().GetText());
        }

        public override double VisitId(LabeledExprParser.IdContext context)
        {
            var id = context.ID().GetText();
            return _memory.ContainsKey(id) 
                ? _memory[id] 
                : 0;
        }

        public override double VisitMulDiv(LabeledExprParser.MulDivContext context)
        {
            var left = Visit(context.expr(0));
            var right = Visit(context.expr(1));
            return context.op.Type == LabeledExprParser.MUL 
                ? left*right 
                : left/right;
        }

        public override double VisitAddSub(LabeledExprParser.AddSubContext context)
        {
            var left = Visit(context.expr(0));
            var right = Visit(context.expr(1));
            return context.op.Type == LabeledExprParser.ADD
                ? left + right
                : left - right;
        }

        public override double VisitParens(LabeledExprParser.ParensContext context)
        {
            return Visit(context.expr());
        }
    }
}