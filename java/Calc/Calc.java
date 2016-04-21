import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.tree.*;
import java.io.FileInputStream;
import java.io.InputStream;

public class Calc {
    public static void main(String[] args) throws Exception {
        InputStream is = (args.length > 0) 
            ?  System.in
            : new FileInputStream(inputFile);
        
        ANTLRInputStream input = new ANTLRInputStream(is);
        LabeledExprLexer lexer = new LabeledExprLexer(input);
        CommonTokenStream tokens = new CommonTokenStream(lexer);
        LabeledExprParser parser = new LabeledExprParser(tokens);
        
        ParseTree tree = parser.prog();
        
        EvalVisitor eval = new EvalVisitor();
        eval.visit(tree);
    }
}